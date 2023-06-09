﻿using System;
using GameplayLogic;
using GameplayLogic.Cells;
using GameplayLogic.PlacedRectangles;
using GameplayLogic.PreviewRectangle;
using SceneManagement;
using UnityEngine;

namespace TouchLogic
{
    [Serializable]
    public class UserActionHandler
    {
        [SerializeField] private TouchReceiver touchReceiver;
        private RectanglePreview rectanglePreview;
        private RectanglePlacingChecker placingChecker;
        private Cell startCell;
        private Cell currentCell;
        private bool canPlace;
        private RectanglesPlacer rectanglesPlacer;

        public void Init(
            RectanglePreview rectanglePreview,
            RectanglePlacingChecker placingChecker)
        {
            this.placingChecker = placingChecker;
            this.rectanglePreview = rectanglePreview;
            rectanglesPlacer = SceneC.Instance.RectanglesPlacer;
            touchReceiver.Init(SceneC.Instance.CameraC.Camera);
            touchReceiver.OnTouchedCell += OnTouchedCell;
            touchReceiver.OnChangedCell += OnChangedCell;
            touchReceiver.OnUnTouchedCell += OnUnTouchedCell;
            touchReceiver.OnClickPlacedCell += OnClickPlacedCell;
            touchReceiver.OnDoubleClickPlacedCell += OnDoubleClickPlacedCell;
        }

        public void Update()
        {
            touchReceiver.Update();
        }

        private void OnTouchedCell(Cell cell)
        {
            startCell = cell;
            if (!startCell.IsMain) return;
            rectanglePreview.Show(cell);
            canPlace = false;
        }

        private void OnChangedCell(Cell cell)
        {
            currentCell = cell;
            if (!startCell.IsMain) return;
            canPlace = placingChecker.CheckPlacement(startCell, cell);
            rectanglePreview.ChangeCell(cell, canPlace);
        }

        private void OnUnTouchedCell()
        {
            if (!startCell.IsMain) return;
            rectanglePreview.Hide();
            if (canPlace)
            {
                rectanglesPlacer.Place(startCell, currentCell);
                SceneC.Instance.GameGrid.CheckWin();
            }
        }

        private void OnClickPlacedCell(PlacedCell placedCell)
        {
            rectanglesPlacer.DeletePreview(placedCell.AttachedRectangle);
        }

        private void OnDoubleClickPlacedCell(PlacedCell placedCell)
        {
            rectanglesPlacer.Delete(placedCell.AttachedRectangle);
        }
    }
}
