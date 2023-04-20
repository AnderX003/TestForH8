using System;
using GameplayLogic;
using GameplayLogic.Cells;
using GameplayLogic.PlacedRectangles;
using SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;

namespace TouchLogic
{
    [Serializable]
    public class TouchReceiver
    {
        public event Action<Cell> OnTouchedCell;
        public event Action<Cell> OnChangedCell;
        public event Action OnUnTouchedCell;

        [SerializeField] private LayerMask hittableLayer;
        private bool isTouching;
        private Camera cam;
        private CellsFinder cellsFinder;
        private PlacedCellsFinder placedCellsFinder;

        private bool started;
        private Cell startCell;
        private Cell currentCell;

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
        }

        public void Update()
        {
            if (!isTouching) return;
            const float maxDistance = 999f;
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, maxDistance, hittableLayer)) return;
            if (placedCellsFinder.TryFindCellByCollider(hit.collider, out var placedCell))
            {
                //HandlePlacedCellRaycast(placedCell);
            }
            else if (cellsFinder.TryFindCellByCollider(hit.collider, out var cell))
            {
                HandleCellRaycast(cell);
            }
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
