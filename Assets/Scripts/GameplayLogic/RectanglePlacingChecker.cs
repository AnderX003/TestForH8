using System;
using GameplayLogic.Cells;
using GridGeneration;
using Helpers.ExtensionsAndDS;
using SceneManagement;

namespace GameplayLogic
{
    public class RectanglePlacingChecker
    {
        private GameGrid gameGrid;

        public void Init()
        {
            gameGrid = SceneC.Instance.GameGrid;
        }

        public bool CheckPlacement(Cell startGameCell, Cell currentGameCell)
        {
            var startCell = startGameCell.GridCell;
            var currentCell = currentGameCell.GridCell;
            if (startCell == currentCell) return false;
            if (!CellsAmountMatches(startCell, currentCell)) return false;
            if (!CheckMainCellsIntersection(startCell, currentCell)) return false;
            if (!CheckRectanglesIntersection(startGameCell, currentGameCell)) return false;
            return true;
        }

        private bool CellsAmountMatches(IGridCell startCell, IGridCell currentCell)
        {
            var margin = startCell.Coords - currentCell.Coords;
            margin = margin.Abs();
            var rectangleSize = (margin.x + 1) * (margin.y + 1);
            return rectangleSize == startCell.Rectangle.CellsAmount;
        }

        private bool CheckMainCellsIntersection(IGridCell startCell, IGridCell currentCell)
        {
            var gridCells = gameGrid.GridCells;
            int minX = Math.Min(startCell.X, currentCell.X);
            int maxX = Math.Max(startCell.X, currentCell.X);
            int minY = Math.Min(startCell.Y, currentCell.Y);
            int maxY = Math.Max(startCell.Y, currentCell.Y);
            for (int x = minX; x <= maxX; x++)
            for (int y = minY; y <= maxY; y++)
            {
                var cell = gridCells[x, y];
                if (cell.IsMain && cell != startCell)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckRectanglesIntersection(Cell startCell, Cell currentCell)
        {
            var gameCells = gameGrid.GameCells;
            int minX = Math.Min(startCell.GridCell.X, currentCell.GridCell.X);
            int maxX = Math.Max(startCell.GridCell.X, currentCell.GridCell.X);
            int minY = Math.Min(startCell.GridCell.Y, currentCell.GridCell.Y);
            int maxY = Math.Max(startCell.GridCell.Y, currentCell.GridCell.Y);
            for (int x = minX; x <= maxX; x++)
            for (int y = minY; y <= maxY; y++)
            {
                var cell = gameCells[x, y];
                if (cell.InPlacedRectangle)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
