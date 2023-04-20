﻿using System;
using System.Collections;
using Helpers.Pooling;
using LevelsManagement;
using SceneManagement;
using UnityEngine;

namespace GameplayLogic.PlacedRectangles
{
    public class PlacedCell : MonoBehaviour, IPoolable
    {
        [SerializeField] private PlacedCellVisualizer visualizer;
        [SerializeField] private new Collider collider;
        private Action onUnloadingLevelAction;

        public PlacedRectangle AttachedRectangle { get; private set; }
        Transform IPoolable.Transform => transform;
        GameObject IPoolable.GameObject => gameObject;

        public IPoolable Init()
        {
            onUnloadingLevelAction ??= OnUnloadingLevel;
            visualizer.Init();
            return this;
        }

        public void Update()
        {
            visualizer.Update();
        }

        public void Enable(PlacedRectangle placedRectangle, Vector3 position)
        {
            LevelsC.Instance.OnUnloadingLevel += onUnloadingLevelAction;
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

        public void Show(Color color)
        {
            visualizer.Show(color);
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
            LevelsC.Instance.OnUnloadingLevel -= onUnloadingLevelAction;
            LevelsC.Instance.PoolsHolder.PlacedCellsPool.PushItem(this);
        }

        public IPoolable HideToPool(Transform poolParent)
        {
            SceneC.Instance?.PlacedCellsFinder.Unregister(this, collider);
            transform.SetParent(poolParent);
            gameObject.SetActive(false);
            return this;
        }

        private void OnUnloadingLevel()
        {
            LevelsC.Instance.OnUnloadingLevel -= onUnloadingLevelAction;
            LevelsC.Instance.PoolsHolder.PlacedCellsPool.PushItem(this);
        }
    }
}
