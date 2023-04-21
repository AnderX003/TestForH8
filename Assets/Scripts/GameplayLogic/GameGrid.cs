using System;
using System.Collections;
using GameplayLogic.Cells;
using GridGeneration;
using Helpers.ExtensionsAndDS;
using LevelsManagement;
using Pools;
using SceneManagement;
using UnityEngine;
using Grid = GridGeneration.Grid;

namespace GameplayLogic
{
    [Serializable]
    public class GameGrid
    {
        [SerializeField] private Transform cellsParent;
        [SerializeField] private CellPlacementConfig cellPlacementConfig;

        private Grid grid;
        private Vector2Int gridSize;
        private IGridCell[,] gridCells;
        private Cell[,] gameCells;

        public Vector2Int GridSize => gridSize;
        public IGridCell[,] GridCells => gridCells;
        public IEnumerable GridRectangles => grid.Rectangles;
        public int GridRectanglesCount => grid.RectanglesCount;
        public Cell[,] GameCells => gameCells;

        public void Init(LevelParams levelParams)
        {
            GenerateGrid(levelParams);
            CreateCells();
            AlignParent();
        }

        private void GenerateGrid(LevelParams levelParams)
        {
            grid = new Grid(levelParams.GridSize);
            grid.DivideToRectangles(levelParams.GenerationLimits);
            gridSize = grid.GridSize;
            gridCells = grid.Cells;
        }

        private void CreateCells()
        {
            gameCells = new Cell[gridSize.x, gridSize.y];
            var pool = LevelsC.Instance.PoolsHolder.CellsPool;
            for (int x = 0; x < gridSize.x; x++)
            for (var y = 0; y < gridSize.y; y++)
            {
                gameCells[x, y] = CreateGameCell(gridCells[x, y], pool);
            }
        }

        private Cell CreateGameCell(IGridCell gridCell, CellsPool pool)
        {
            var gameCell = pool.PopItem(cellsParent);
            gameCell.Init(gridCell);
            gameCell.Arrange(cellPlacementConfig);
            return gameCell;
        }

        private void AlignParent()
        {
            var cellStart = gameCells[0, 0];
            var cellEnd = gameCells[gridSize.x - 1, gridSize.y - 1];
            var averagePos = (cellStart.GetPosition() +
                              cellEnd.GetPosition()) / 2f;
            cellsParent.position = cellsParent.position
                .WithX(-averagePos.x)
                .WithZ(-averagePos.z);
        }

        public void CheckWin()
        {
            for (int x = 0; x < gridSize.x; x++)
            for (var y = 0; y < gridSize.y; y++)
            {
                if (!gameCells[x, y].InPlacedRectangle) return;
            }
            SceneC.Instance.GameLoopC.Win();
        }
    }
}
