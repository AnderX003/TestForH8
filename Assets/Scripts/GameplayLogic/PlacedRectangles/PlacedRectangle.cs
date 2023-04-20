﻿using System;
using System.Collections;
using GameplayLogic.Cells;
using SceneManagement;
using UnityEngine;

namespace GameplayLogic.PlacedRectangles
{
    public class PlacedRectangle
    {
        private readonly Vector2Int size;
        private readonly Cell startCell;
        private readonly Cell endCell;
        private readonly Cell[,] cells;
        private readonly PlacedCell[,] placedCells;

        public PlacedRectangle(GameGrid gameGrid, Cell startCell, Cell endCell)
        {
            this.startCell = startCell;
            this.endCell = endCell;
            var startGridCell = startCell.GridCell;
            var endGridCell = endCell.GridCell;

            size = new Vector2Int(
                Mathf.Abs(startGridCell.X - endGridCell.X) + 1,
                Mathf.Abs(startGridCell.Y - endGridCell.Y) + 1);
            placedCells = new PlacedCell[size.x, size.y];
            cells = new Cell[size.x, size.y];
            var gameCells = gameGrid.GameCells;

            IterateSubMatrix(
                startGridCell.X, startGridCell.Y,
                endGridCell.X, endGridCell.Y,
                (x, y, xLocal, yLocal) =>
                {
                    var gameCell = gameCells[x, y];
                    cells[xLocal, yLocal] = gameCell;
                    gameCell.BindPlacedRectangle(this);
                });
        }

        private void IterateSubMatrix(int x1,int y1,int x2,int y2, Action<int, int, int, int> action)
        {
            var dx = Math.Sign(x2 - x1);
            var dy = Math.Sign(y2 - y1);
            if (dy == 0) dy = 1;
            if (dx == 0) dx = 1;
            for (int x = x1, xLocal = 0; x != x2 + dx; x += dx, xLocal++)
            for (int y = y1, yLocal = 0; y != y2 + dy; y += dy, yLocal++)
            {
                action(x, y, xLocal, yLocal);
            }
        }

        public void Place(float showInterval)
        {
            var pool = SceneC.Instance.PoolsHolder.PlacedCellsPool;
            for (int x = 0; x < size.x; x++)
            for (int y = 0; y < size.y; y++)
            {
                var placedCell = pool.PopItem();
                placedCells[x, y] = placedCell;
                placedCell.Enable(cells[x, y].GetPosition());
            }
            SceneC.Instance.StartCoroutine(ShowSequence(showInterval));
        }

        private IEnumerator ShowSequence(float showInterval)
        {
            var wait = new WaitForSeconds(showInterval);
            var x1 = startCell.GridCell.X;
            var y1 = startCell.GridCell.Y;
            var x2 = endCell.GridCell.X;
            var y2 = endCell.GridCell.Y;
            var dx = Math.Sign(x2 - x1);
            var dy = Math.Sign(y2 - y1);
            if (dy == 0) dy = 1;
            if (dx == 0) dx = 1;
            for (int x = x1, xLocal = 0; x != x2 + dx; x += dx, xLocal++)
            for (int y = y1, yLocal = 0; y != y2 + dy; y += dy, yLocal++)
            {
                placedCells[xLocal, yLocal].Show();
                yield return wait;
            }
        }

        public void UnPlace(float hideInterval)
        {
            var x1 = startCell.GridCell.X;
            var y1 = startCell.GridCell.Y;
            var x2 = endCell.GridCell.X;
            var y2 = endCell.GridCell.Y;
            var dx = Math.Sign(x2 - x1);
            var dy = Math.Sign(y2 - y1);
            if (dy == 0) dy = 1;
            if (dx == 0) dx = 1;
            for (int x = x1, xLocal = 0; x != x2 + dx; x += dx, xLocal++)
            for (int y = y1, yLocal = 0; y != y2 + dy; y += dy, yLocal++)
            {
                placedCells[xLocal, yLocal].Disable();
                cells[x, y].UnBindPlacedRectangle();
            }
            SceneC.Instance.StartCoroutine(HideSequence(hideInterval));
        }

        private IEnumerator HideSequence(float hideInterval)
        {
            var wait = new WaitForSeconds(hideInterval);
            var x1 = endCell.GridCell.X;
            var y1 = endCell.GridCell.Y;
            var x2 = startCell.GridCell.X;
            var y2 = startCell.GridCell.Y;
            var dx = Math.Sign(x2 - x1);
            var dy = Math.Sign(y2 - y1);
            if (dy == 0) dy = 1;
            if (dx == 0) dx = 1;
            for (int x = x1, xLocal = 0; x != x2 + dx; x += dx, xLocal++)
            for (int y = y1, yLocal = 0; y != y2 + dy; y += dy, yLocal++)
            {
                placedCells[xLocal, yLocal].Hide();
                yield return wait;
            }
        }

        public void PunchScale()
        {
            for (int x = 0; x < size.x; x++)
            for (int y = 0; y < size.y; y++)
            {
                placedCells[x, y].PunchScale();
            }
        }
    }
}
