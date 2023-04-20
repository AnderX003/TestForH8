using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using UnityEngine;

namespace GridGeneration
{
    public class Grid
    {
        private Vector2Int gridSize;
        private List<Rectangle> rectangles;
        private GridCell[,] cells;

        public IEnumerable<IGridRectangle> Rectangles => rectangles;
        public IGridCell[,] Cells => cells;
        public Vector2Int GridSize => gridSize;

        public int RectanglesCount => rectangles.Count;

        public Grid(Vector2Int gridSize)
        {
            this.gridSize = gridSize;
            cells = new GridCell[gridSize.x, gridSize.y];
            for (int x = 0; x < gridSize.x; x++)
            for (var y = 0; y < gridSize.y; y++)
            {
                cells[x, y] = new GridCell(x, y);
            }
        }

        public void DivideToRectangles(RectangleGenerationLimits rectangleLimits)
        {
            rectangles = new List<Rectangle>();
            DivideToBasicRectangles(rectangleLimits);
            FixSingleCells();
        }

        private void DivideToBasicRectangles(RectangleGenerationLimits rectangleLimits)
        {
            for (int x = 0; x < gridSize.x; x++)
            for (var y = 0; y < gridSize.y; y++)
            {
                var cell = cells[x, y];
                if (cell.IsOccupied) continue;
                if (CellHasNoWays(cell)) continue;

                var rectangle = new Rectangle(cell);
                rectangle.Build(cells, gridSize, rectangleLimits);
                rectangles.Add(rectangle);
            }
        }

        private void FixSingleCells()
        {
            for (var y = 0; y < gridSize.y; y++)
            {
                var cell = cells[gridSize.x - 1, y];
                if (cell.IsOccupied) continue;

                JoinSingleCell(cell);
            }
        }

        [Pure]
        private bool CellHasNoWays(GridCell cell)
        {
            return CellIsOnLastRow(cell) && CellHasNoFreeNeighbor(cell);
        }

        [Pure]
        private bool CellIsOnLastRow(GridCell cell)
        {
            return cell.X == gridSize.x - 1;
        }

        [Pure]
        private bool CellHasNoFreeNeighbor(GridCell cell)
        {
            var rightCell = GetCellOrNull(cell.X, cell.Y + 1);
            return rightCell == null || rightCell.IsOccupied;
        }

        [Pure]
        private GridCell GetCellOrNull(int x, int y)
        {
            if (x < 0 || x >= gridSize.x ||
                y < 0 || y >= gridSize.y) return null;
            return cells[x, y];
        }

        private void JoinSingleCell(GridCell cell)
        {
            // adding single cell to nearest fitting rectangle
            var upCell = GetCellOrNull(cell.X - 1, cell.Y);
            var leftCell = GetCellOrNull(cell.X, cell.Y - 1);
            var rightCell = GetCellOrNull(cell.X, cell.Y + 1);

            if (upCell != null && upCell.Rectangle.Size.y == 1)
            {
                upCell.Rectangle.AddExtraCell(cells, Rectangle.AddSide.Down);
            }
            else if (leftCell != null && leftCell.Rectangle.Size.x == 1)
            {
                leftCell.Rectangle.AddExtraCell(cells, Rectangle.AddSide.Right);
            }
            else if (rightCell != null && rightCell.Rectangle.Size.x == 1)
            {
                rightCell.Rectangle.AddExtraCell(cells, Rectangle.AddSide.Left);
            }
            else
            {
                Debug.LogError("Unexpected");
            }
        }

#if UNITY_EDITOR
        public override string ToString()
        {
            var msg = new StringBuilder();
            for (int x = 0; x < gridSize.x; x++)
            {
                for (var y = 0; y < gridSize.y; y++)
                {
                    msg.Append($"{cells[x, y].Rectangle?.Id.ToString() ?? "-"}\t");
                }
                msg.Append("\n");
            }
            return msg.ToString();
        }
#endif
    }
}
