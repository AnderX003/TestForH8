using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Assertions;

namespace FieldGeneration
{
    public class Rectangle
    {
        private static int idCounter;

        private Vector2Int rootCoord;
        private Cell mainCell;
        private Cell[,] cells;
        private Vector2Int size;

        public Vector2Int Size => size;
        public int Id { get; }

        public Rectangle(Cell rootCell)
        {
            rootCoord = new Vector2Int(rootCell.X, rootCell.Y);
            Id = idCounter++;
        }

        public void Build(Cell[,] field, Vector2Int fieldSize, RectangleGenerationLimits limits)
        {
            var maxSize = CalculateMaxSize(field, fieldSize);
            var maxArea = maxSize.x * maxSize.y;
            Assert.AreNotEqual(maxArea, 1);
            GenerateRectangleSize(limits, maxSize);
            PickCells(field);
            PickMainCell();
        }

        [Pure]
        private Vector2Int CalculateMaxSize(Cell[,] field, Vector2Int fieldSize)
        {
            var maxCoords = FindMaxRectangleCoords(field, fieldSize);
            var maxSize = new Vector2Int(
                maxCoords.x - rootCoord.x + 1,
                maxCoords.y - rootCoord.y + 1);
            return maxSize;
        }

        [Pure]
        private Vector2Int FindMaxRectangleCoords(Cell[,] field, Vector2Int fieldSize)
        {
            var maxCoords = new Vector2Int(
                fieldSize.x - 1,
                rootCoord.y);
            if (rootCoord.y == fieldSize.y - 1) return maxCoords;

            for (int y = rootCoord.y + 1; y < fieldSize.y; y++)
            {
                if (!field[rootCoord.x, y].IsOccupied)
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

        private void PickCells(Cell[,] field)
        {
            cells = new Cell[size.x, size.y];
            for (int x = rootCoord.x; x < rootCoord.x + size.x; x++)
            {
                for (var y = rootCoord.y; y < rootCoord.y + size.y; y++)
                {
                    var localCoords = LocalCoords(x, y);
                    var cell = field[x, y];
                    cell.AttachRectangle(this);
                    cells[localCoords.x, localCoords.y] = cell;
                }
            }
        }

        private void PickMainCell()
        {
            mainCell = cells[
                Random.Range(0, Size.x),
                Random.Range(0, Size.y)];
        }

        public enum AddSide : byte { Down, Right, Left }

        public void AddExtraCell(Cell[,] field, AddSide addSide)
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
            PickCells(field);
            PickMainCell();
        }
    }
}
