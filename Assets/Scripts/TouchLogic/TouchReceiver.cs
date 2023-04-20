using System;
using GameplayLogic.Cells;
using GameplayLogic.PlacedRectangles;
using SceneManagement;
using UnityEngine;

namespace TouchLogic
{
    [Serializable]
    public class TouchReceiver
    {
        public event Action<PlacedCell> OnDoubleClickPlacedCell;
        public event Action<PlacedCell> OnClickPlacedCell;
        public event Action<Cell> OnTouchedCell;
        public event Action<Cell> OnChangedCell;
        public event Action OnUnTouchedCell;

        [SerializeField] private LayerMask cellsLayer;
        [SerializeField] private LayerMask rectanglesLayer;
        [SerializeField] private float doubleClickTimeThreshold;

        private bool isTouching;
        private bool started;
        private float lastClickTime;
        private Camera cam;
        private Cell startCell;
        private Cell currentCell;
        private CellsFinder cellsFinder;
        private PlacedCellsFinder placedCellsFinder;

        public void Init(Camera cam)
        {
            this.cam = cam;
            var gameUI = SceneC.Instance.UIHolder.GameUI;
            gameUI.OnDragTriggerDown += OnDragDown;
            gameUI.OnDragTriggerUp += OnDragUp;
            cellsFinder = SceneC.Instance.CellsFinder;
            placedCellsFinder = SceneC.Instance.PlacedCellsFinder;
        }

        private void OnDragDown()
        {
            isTouching = true;
        }

        private void OnDragUp()
        {
            isTouching = false;
            if (started)
            {
                started = false;
                startCell = null;
                currentCell = null;
                OnUnTouchedCell?.Invoke();
            }

            HandleClick();
            lastClickTime = Time.time;
        }

        private void HandleClick()
        {
            if (!Raycast(out var hit, rectanglesLayer)) return;
            if (placedCellsFinder.TryFindCellByCollider(hit.collider, out var placedCell))
            {
                float timeSinceLastClick = Time.time - lastClickTime;
                if (timeSinceLastClick <= doubleClickTimeThreshold)
                {
                    OnDoubleClickPlacedCell?.Invoke(placedCell);
                }
                else
                {
                    OnClickPlacedCell?.Invoke(placedCell);
                }
            }
        }

        public void Update()
        {
            if (!isTouching) return;
            if (!Raycast(out var hit, cellsLayer)) return;
            if (cellsFinder.TryFindCellByCollider(hit.collider, out var cell))
            {
                HandleCellRaycast(cell);
            }
        }

        private bool Raycast(out RaycastHit hit, LayerMask layer)
        {
            const float maxDistance = 999f;
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hit, maxDistance, layer);
        }

        private void HandleCellRaycast(Cell cell)
        {
            if (cell.InPlacedRectangle) return;
            if (!started)
            {
                started = true;
                startCell = cell;
                currentCell = cell;
                OnTouchedCell?.Invoke(startCell);
            }
            else if (currentCell != cell)
            {
                currentCell = cell;
                OnChangedCell?.Invoke(cell);
            }
        }
    }
}
