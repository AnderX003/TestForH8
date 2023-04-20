using System;
using GameplayObjects;
using SceneManagement;
using UnityEngine;

namespace TouchLogic
{
    [Serializable]
    public class TouchReceiver
    {
        public event Action<Cell> OnTouchedCell;
        public event Action<Cell> OnChangedCell;
        public event Action OnUnTouchedCell;

        [SerializeField] private LayerMask cellsLayer;
        private bool isTouching;
        private Camera cam;
        private CellsFinder cellsFinder;

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
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 999f, cellsLayer) &&
                cellsFinder.TryFindCellByCollider(hit.collider, out var cell))
            {
                HandleCellRaycast(cell);
            }
        }

        private void HandleCellRaycast(Cell cell)
        {
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
