using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using UnityEngine;

namespace FieldGeneration
{
    public class Field
    {
        private Vector2Int fieldSize;
        public List<Rectangle> rectangles { get; private set; }
        public Cell[,] field { get; }

        public Field(Vector2Int fieldSize)
        {
            this.fieldSize = fieldSize;
            field = new Cell[fieldSize.x, fieldSize.y];
            for (int x = 0; x < fieldSize.x; x++)
            {
                for (var y = 0; y < fieldSize.y; y++)
                {
                    field[x, y] = new Cell(x, y);
                }
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
            for (int x = 0; x < fieldSize.x; x++)
            {
                for (var y = 0; y < fieldSize.y; y++)
                {
                    var cell = field[x, y];
                    if (cell.IsOccupied) continue;
                    if (CellHasNoWays(cell)) continue;

                    var rectangle = new Rectangle(cell);
                    rectangle.Build(field, fieldSize, rectangleLimits);
                    rectangles.Add(rectangle);
                }
            }
        }

        private void FixSingleCells()
        {
            for (var y = 0; y < fieldSize.y; y++)
            {
                var cell = field[fieldSize.x - 1, y];
                if (cell.IsOccupied) continue;

                JoinSingleCell(cell);
            }
        }

        [Pure]
        private bool CellHasNoWays(Cell cell)
        {
            return CellIsOnLastRow(cell) && CellHasNoFreeNeighbor(cell);
        }

        [Pure]
        private bool CellIsOnLastRow(Cell cell)
        {
            return cell.X == fieldSize.x - 1;
        }

        [Pure]
        private bool CellHasNoFreeNeighbor(Cell cell)
        {
            var rightCell = GetCellOrNull(cell.X, cell.Y + 1);
            return rightCell == null || rightCell.IsOccupied;
        }

        [Pure]
        private Cell GetCellOrNull(int x, int y)
        {
            if (x < 0 || x >= fieldSize.x ||
                y < 0 || y >= fieldSize.y) return null;
            return field[x, y];
        }

        private void JoinSingleCell(Cell cell)
        {
            // adding single cell to nearest fitting rectangle
            var upCell = GetCellOrNull(cell.X - 1, cell.Y);
            var leftCell = GetCellOrNull(cell.X, cell.Y - 1);
            var rightCell = GetCellOrNull(cell.X, cell.Y + 1);

            if (upCell != null && upCell.Rectangle.Size.y == 1)
            {
                upCell.Rectangle.AddExtraCell(field, Rectangle.AddSide.Down);
            }
            else if (leftCell != null && leftCell.Rectangle.Size.x == 1)
            {
                leftCell.Rectangle.AddExtraCell(field, Rectangle.AddSide.Right);
            }
            else if (rightCell != null && rightCell.Rectangle.Size.x == 1)
            {
                rightCell.Rectangle.AddExtraCell(field, Rectangle.AddSide.Left);
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
            for (int x = 0; x < fieldSize.x; x++)
            {
                for (var y = 0; y < fieldSize.y; y++)
                {
                    msg.Append($"{field[x, y].Rectangle?.Id.ToString() ?? "-"}\t");
                }
                msg.Append("\n");
            }
            return msg.ToString();
        }
#endif
    }
}
