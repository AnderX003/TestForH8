using System;
using GridGeneration;
using LevelsManagement;
using SceneManagement;
using SceneManagement.Pools;
using UnityEngine;
using Grid = GridGeneration.Grid;

namespace GameplayObjects
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

        public void Init(LevelParams levelParams)
        {
            GenerateGrid(levelParams);
            CreateCells();
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
            var pool = SceneC.Instance.PoolsHolder.CellsPool;
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
    }
}
