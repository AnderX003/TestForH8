using System;
using DG.Tweening;
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
        [SerializeField] private DoAnimationParams<Ease> buttonShowParams;
        [SerializeField] private DoAnimationParams<Ease> buttonHideParams;

        private TweenCallback disableAction;

        public void Init()
        {
            var sceneC = SceneC.Instance;
            dragTrigger.AddEntry(EventTriggerType.PointerDown, DragTriggerDown);
            dragTrigger.AddEntry(EventTriggerType.PointerUp, DragTriggerUp);
            pauseButton.onClick.AddListener(PauseButtonClick);
            var gameLoopC = sceneC.GameLoopC;
            gameLoopC.OnPause += OnPause;
            gameLoopC.OnResume += OnResume;
            gameLoopC.OnWin += OnWin;
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
            Hide();
        }

        private void OnWin()
        {
            Hide();
        }

        private void Hide()
        {
            disableAction ??= Disable;
            pauseButton.transform
                .DOScale(Vector3.zero, buttonHideParams.Duration)
                .SetEase(buttonHideParams.Ease)
                .OnComplete(disableAction);
        }

        private void Disable()
        {
            gamePanel.SetActive(false);
        }

        private void OnResume()
        {
            Show();
        }

        private void Show()
        {
            gamePanel.SetActive(true);
            pauseButton.transform
                .DOScale(Vector3.one, buttonShowParams.Duration)
                .SetEase(buttonShowParams.Ease);
        }
    }
}
