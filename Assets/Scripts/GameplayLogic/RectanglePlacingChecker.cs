using GridGeneration;
using Helpers.ExtensionsAndDS;

namespace GameplayLogic
{
    public class RectanglePlacingChecker
    {
        private GameGrid gameGrid;

        public void Init(GameGrid gameGrid)
        {
            this.gameGrid = gameGrid;
        }

        public bool CheckPlacement(IGridCell startCell, IGridCell currentCell)
        {
            if (startCell == currentCell) return false;
            if (!CellsAmountMatches(startCell, currentCell)) return false;
            if (!CheckMainCellsIntersection(startCell, currentCell)) return false;
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
            for (int x = startCell.X; x <= currentCell.X; x++)
            for (int y = startCell.Y; y <= currentCell.Y; y++)
            {
                var cell = gridCells[x, y];
                if (cell.IsMain && cell != startCell)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
