using System;
using GameplayLogic.PlacedRectangles;
using GridGeneration;
using Helpers.Pooling;
using LevelsManagement;
using SceneManagement;
using TMPro;
using UnityEngine;

namespace GameplayLogic.Cells
{
    public class Cell : MonoBehaviour, IPoolable
    {
        [SerializeField] private new Collider collider;
        [SerializeField] private TMP_Text numberText;

        private IGridCell gridCell;
        private Action onUnloadingLevelAction;

        public IGridCell GridCell => gridCell;
        public bool IsMain => gridCell.IsMain;
        public Rectangle Rectangle => gridCell.Rectangle;
        public bool InPlacedRectangle { get; private set; }
        public PlacedRectangle PlacedRectangle { get; private set; }

        public int RectangleSize
        {
            get
            {
                var size = gridCell.Rectangle.Size;
                return size.x * size.y;
            }
        }

        Transform IPoolable.Transform => transform;
        GameObject IPoolable.GameObject => gameObject;

        public void Init(IGridCell gridCell)
        {
            this.gridCell = gridCell;
            onUnloadingLevelAction ??= OnUnloadingLevel;
            LevelsC.Instance.OnUnloadingLevel += onUnloadingLevelAction;
            SceneC.Instance.CellsFinder.Register(this, collider);
            if (gridCell.IsMain)
            {
                ActivateAsMainSell();
            }
            gameObject.name = $"Cell{{{gridCell.X.ToString()}; {gridCell.Y.ToString()}}})";
        }

        public void Arrange(CellPlacementConfig config)
        {
            var offset = gridCell.Coords * config.Step;
            transform.position =
                new Vector3(config.StartPos.y, 0f, config.StartPos.x) +
                new Vector3(offset.y, 0f, offset.x);
        }

        private void ActivateAsMainSell()
        {
            numberText.gameObject.SetActive(true);
            numberText.SetText(RectangleSize.ToString());
        }

        public IPoolable Setup(Transform parent)
        {
            InPlacedRectangle = false;
            PlacedRectangle = null;
            numberText.gameObject.SetActive(false);
            transform.SetParent(parent);
            gameObject.SetActive(true);
            return this;
        }

        public IPoolable HideToPool(Transform poolParent)
        {
            SceneC.Instance?.CellsFinder.Unregister(this, collider);
            transform.SetParent(poolParent);
            gameObject.SetActive(false);
            return this;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public Vector2Int GetGridCoords()
        {
            return gridCell.Coords;
        }

        public void BindPlacedRectangle(PlacedRectangle placedRectangle)
        {
            InPlacedRectangle = true;
            PlacedRectangle = placedRectangle;
        }

        public void UnBindPlacedRectangle()
        {
            InPlacedRectangle = false;
            PlacedRectangle = null;
        }

        private void OnUnloadingLevel()
        {
            LevelsC.Instance.OnUnloadingLevel -= onUnloadingLevelAction;
            LevelsC.Instance.PoolsHolder.CellsPool.PushItem(this);
        }
    }
}
