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

        public void Enable(Vector3 position)
        {
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
    }
}
