using System;
using GameplayObjects;
using GameplayObjects.PreviewRectangle;
using SceneManagement;
using UnityEngine;

namespace TouchLogic
{
    [Serializable]
    public class TouchHandler
    {
        [SerializeField] private TouchReceiver touchReceiver;
        private RectanglePreview rectanglePreview;
        private RectanglePlacingChecker placingChecker;
        private Cell startCell;
        private bool canPlace;

        public void Init(
            RectanglePreview rectanglePreview,
            RectanglePlacingChecker placingChecker)
        {
            this.placingChecker = placingChecker;
            this.rectanglePreview = rectanglePreview;
            touchReceiver.Init(SceneC.Instance.CameraC.Camera);
            touchReceiver.OnTouchedCell += OnTouchedCell;
            touchReceiver.OnChangedCell += OnChangedCell;
            touchReceiver.OnUnTouchedCell += OnUnTouchedCell;
        }

        public void Update()
        {
            touchReceiver.Update();
        }

        private void OnTouchedCell(Cell cell)
        {
            startCell = cell;
            if (!startCell.IsMain) return;
            Debug.Log($"OnTouchedCell {cell.name}");
            rectanglePreview.Show(cell);
            canPlace = false;
        }

        private void OnChangedCell(Cell cell)
        {
            if (!startCell.IsMain) return;
            Debug.Log($"OnChangedCell {cell.name}");
            canPlace = placingChecker.CheckPlacement(startCell.GridCell, cell.GridCell);
            rectanglePreview.ChangeCell(cell, canPlace);
        }

        private void OnUnTouchedCell()
        {
            if (!startCell.IsMain) return;
            Debug.Log("OnUnTouchedCell");
            rectanglePreview.Hide();
            if (canPlace)
            {
                //todo place
            }
        }
    }
}
