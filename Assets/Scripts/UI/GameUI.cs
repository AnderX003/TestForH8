using System;
using Helpers.ExtensionsAndDS;
using SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class GameUI
    {
        public event Action OnDragTriggerDown;
        public event Action OnDragTriggerUp;

        [SerializeField] private GameObject gamePanel;
        [SerializeField] private EventTrigger dragTrigger;
        [SerializeField] private Button pauseButton;

        public void Init()
        {
            var sceneC = SceneC.Instance;
            dragTrigger.AddEntry(EventTriggerType.PointerDown, DragTriggerDown);
            dragTrigger.AddEntry(EventTriggerType.PointerUp, DragTriggerUp);
            pauseButton.onClick.AddListener(PauseButtonClick);
            sceneC.GameLoopC.OnPause += OnPause;
            sceneC.GameLoopC.OnResume += OnResume;
        }

        public void DragTriggerDown(BaseEventData _)
        {
            OnDragTriggerDown?.Invoke();
        }

        public void DragTriggerUp(BaseEventData _)
        {
            OnDragTriggerUp?.Invoke();
        }

        private void PauseButtonClick()
        {
            SceneC.Instance.GameLoopC.Pause();
        }

        private void OnPause()
        {
            gamePanel.SetActive(false);
        }

        private void OnResume()
        {
            gamePanel.SetActive(true);
        }
    }
}
