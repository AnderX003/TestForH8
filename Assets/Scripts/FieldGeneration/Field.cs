﻿using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FieldGeneration
{
    public class Field
    {
        private Vector2Int gridSize;
        private List<Rectangle> rectangles;
        private GridCell[,] grid;

        public IEnumerable<IGridRectangle> Rectangles => rectangles;
        public IGridCell[,] Grid => grid;

        public Field(Vector2Int gridSize)
        {
            this.gridSize = gridSize;
            grid = new GridCell[gridSize.x, gridSize.y];
            for (int x = 0; x < gridSize.x; x++)
            {
                for (var y = 0; y < gridSize.y; y++)
                {
                    grid[x, y] = new GridCell(x, y);
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
            for (int x = 0; x < gridSize.x; x++)
            {
                for (var y = 0; y < gridSize.y; y++)
                {
                    var cell = grid[x, y];
                    if (cell.IsOccupied) continue;
                    if (CellHasNoWays(cell)) continue;

                    var rectangle = new Rectangle(cell);
                    rectangle.Build(grid, gridSize, rectangleLimits);
                    rectangles.Add(rectangle);
                }
            }
        }

        private void FixSingleCells()
        {
            for (var y = 0; y < gridSize.y; y++)
            {
                var cell = grid[gridSize.x - 1, y];
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
            return grid[x, y];
        }

        private void JoinSingleCell(GridCell cell)
        {
            // adding single cell to nearest fitting rectangle
            var upCell = GetCellOrNull(cell.X - 1, cell.Y);
            var leftCell = GetCellOrNull(cell.X, cell.Y - 1);
            var rightCell = GetCellOrNull(cell.X, cell.Y + 1);

            if (upCell != null && upCell.Rectangle.Size.y == 1)
            {
                upCell.Rectangle.AddExtraCell(grid, Rectangle.AddSide.Down);
            }
            else if (leftCell != null && leftCell.Rectangle.Size.x == 1)
            {
                leftCell.Rectangle.AddExtraCell(grid, Rectangle.AddSide.Right);
            }
            else if (rightCell != null && rightCell.Rectangle.Size.x == 1)
            {
                rightCell.Rectangle.AddExtraCell(grid, Rectangle.AddSide.Left);
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
                    msg.Append($"{grid[x, y].Rectangle?.Id.ToString() ?? "-"}\t");
                }
                msg.Append("\n");
            }
            return msg.ToString();
        }
#endif
    }
}
