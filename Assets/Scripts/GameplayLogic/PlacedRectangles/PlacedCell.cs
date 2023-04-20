using System.Collections;
using Helpers.Pooling;
using SceneManagement;
using UnityEngine;

namespace GameplayLogic.PlacedRectangles
{
    public class PlacedCell : MonoBehaviour, IPoolable
    {
        [SerializeField] private PlacedCellVisualizer visualizer;
        [SerializeField] private new Collider collider;

        public PlacedRectangle AttachedRectangle { get; private set; }
        Transform IPoolable.Transform => transform;
        GameObject IPoolable.GameObject => gameObject;

        public IPoolable Init()
        {
            visualizer.Init();
            return this;
        }

        public void Update()
        {
            visualizer.Update();
        }

        public void Enable(PlacedRectangle placedRectangle, Vector3 position)
        {
            SceneC.Instance.PlacedCellsFinder.Register(this, collider);
            AttachedRectangle = placedRectangle;
            transform.localPosition = position;
            collider.enabled = true;
            visualizer.ResetOnEnable();
        }

        public void Disable()
        {
            collider.enabled = false;
        }

        public void Show()
        {
            visualizer.Show();
        }

        public void PunchScale()
        {
            visualizer.PunchScale();
        }

        public void Hide()
        {
            StartCoroutine(HideCoroutine());
        }

        private IEnumerator HideCoroutine()
        {
            yield return StartCoroutine(visualizer.Hide());
            SceneC.Instance.PoolsHolder.PlacedCellsPool.PushItem(this);
        }

        public IPoolable HideToPool(Transform poolParent)
        {
            SceneC.Instance.PlacedCellsFinder.Unregister(this, collider);
            transform.SetParent(poolParent);
            gameObject.SetActive(false);
            return this;
        }
    }
}
