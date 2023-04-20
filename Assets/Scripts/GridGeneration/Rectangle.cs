using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace GridGeneration
{
    public class Rectangle : IGridRectangle
    {
        private static int idCounter;

        private GridCell mainCell;
        private GridCell[,] cells;
        private Vector2Int rootCoord;
        private Vector2Int size;

        public int CellsAmount => cells.Length;
        public Vector2Int RootCoord => rootCoord;
        public Vector2Int Size => size;
        public IGridCell MainCell => mainCell;
        public IEnumerable<IGridCell> Cells => cells.Cast<IGridCell>();
        public int Id { get; }

        public Rectangle(GridCell rootCell)
        {
            rootCoord = new Vector2Int(rootCell.X, rootCell.Y);
            Id = idCounter++;
        }

        public void Build(GridCell[,] grid, Vector2Int gridSize, RectangleGenerationLimits limits)
        {
            var maxSize = CalculateMaxSize(grid, gridSize);
            var maxArea = maxSize.x * maxSize.y;
            Assert.AreNotEqual(maxArea, 1);
            GenerateRectangleSize(limits, maxSize);
            PickCells(grid);
            PickMainCell();
        }

        [Pure]
        private Vector2Int CalculateMaxSize(GridCell[,] grid, Vector2Int gridSize)
        {
            var maxCoords = FindMaxRectangleCoords(grid, gridSize);
            var maxSize = new Vector2Int(
                maxCoords.x - rootCoord.x + 1,
                maxCoords.y - rootCoord.y + 1);
            return maxSize;
        }

        [Pure]
        private Vector2Int FindMaxRectangleCoords(GridCell[,] grid, Vector2Int gridSize)
        {
            var maxCoords = new Vector2Int(
                gridSize.x - 1,
                rootCoord.y);
            if (rootCoord.y == gridSize.y - 1) return maxCoords;

            for (int y = rootCoord.y + 1; y < gridSize.y; y++)
            {
                if (!grid[rootCoord.x, y].IsOccupied)
                {
                    maxCoords.y++;
                }
                else
                {
                    break;
                }
            }
            return maxCoords;
        }

        private void GenerateRectangleSize(RectangleGenerationLimits limits, Vector2Int maxSize)
        {
            do
            {
                size = new Vector2Int(
                    Random.Range(1, maxSize.x + 1),
                    Random.Range(1, maxSize.y + 1));
            } while (CheckLimits(limits));
        }

        [Pure]
        private bool CheckLimits(RectangleGenerationLimits limits)
        {
            return size.x * size.y < limits.minRectangleArea ||
                   size.x * size.y > limits.maxRectangleArea ||
                   size.x > limits.maxRectangleLength ||
                   size.y > limits.maxRectangleLength;
        }

        [Pure]
        private Vector2Int LocalCoords(int absoluteX, int absoluteY)
        {
            return new Vector2Int(
                absoluteX - rootCoord.x,
                absoluteY - rootCoord.y);
        }

        private void PickCells(GridCell[,] grid)
        {
            cells = new GridCell[size.x, size.y];
            for (int x = rootCoord.x; x < rootCoord.x + size.x; x++)
            for (var y = rootCoord.y; y < rootCoord.y + size.y; y++)
            {
                var localCoords = LocalCoords(x, y);
                var cell = grid[x, y];
                cell.AttachRectangle(this);
                cells[localCoords.x, localCoords.y] = cell;
            }
        }

        private void PickMainCell()
        {
            if (mainCell != null)
            {
                mainCell.IsMain = false;
            }

            mainCell = Random.Range(0, 4) switch
            {
                0 => cells[0, 0],
                1 => cells[size.x - 1, 0],
                2 => cells[0, size.y - 1],
                3 => cells[size.x - 1, size.y - 1],
                _ => throw new ArgumentOutOfRangeException()
            };
            mainCell.IsMain = true;
        }

        public enum AddSide : byte { Down, Right, Left }

        public void AddExtraCell(GridCell[,] grid, AddSide addSide)
        {
            switch (addSide)
            {
                case AddSide.Down:
                    size.x++;
                    break;
                case AddSide.Right:
                    size.y++;
                    break;
                case AddSide.Left:
                    rootCoord.y--;
                    size.y++;
                    break;
            }
            PickCells(grid);
            PickMainCell();
        }
    }
}
