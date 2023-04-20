using System;
using DG.Tweening;
using Helpers.ExtensionsAndDS;
using SceneManagement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class PauseUI
    {
        [SerializeField] private Image pausePanel;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private float restartButtonAppearDelay;
        [SerializeField] private DoAnimationParams<Ease> panelShowParams;
        [SerializeField] private DoAnimationParams<Ease> buttonsShowParams;
        [SerializeField] private DoAnimationParams<Ease> panelHideParams;
        [SerializeField] private DoAnimationParams<Ease> buttonsHideParams;

        private TweenCallback disableAction;
        private Color panelDefaultColor;

        public void Init()
        {
            panelDefaultColor = pausePanel.color;
            SceneC.Instance.GameLoopC.OnPause += OnPause;
            resumeButton.onClick.AddListener(OnResumeButtonClick);
            restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        public void OnPause()
        {
            pausePanel.gameObject.SetActive(true);

            pausePanel.color = pausePanel.color.WithAlpha(0f);
            resumeButton.transform.localScale = Vector3.zero;
            restartButton.transform.localScale = Vector3.zero;

            DOTween.Sequence()
                .Append(pausePanel
                    .DOFade(panelDefaultColor.a, panelShowParams.Duration)
                    .SetEase(panelShowParams.Ease))
                .Append(resumeButton.transform
                    .DOScale(Vector3.one, buttonsShowParams.Duration)
                    .SetEase(buttonsShowParams.Ease))
                .Join(restartButton.transform
                    .DOScale(Vector3.one, buttonsShowParams.Duration)
                    .SetEase(buttonsShowParams.Ease)
                    .SetDelay(restartButtonAppearDelay));
        }

        private void OnResumeButtonClick()
        {
            Hide();
            SceneC.Instance.GameLoopC.Resume();
        }

        private void Hide()
        {
            disableAction ??= Disable;
            DOTween.Sequence()
                .Append(pausePanel
                    .DOFade(0f, panelHideParams.Duration)
                    .SetEase(panelHideParams.Ease))
                .Join(resumeButton.transform
                    .DOScale(Vector3.zero, buttonsHideParams.Duration)
                    .SetEase(buttonsHideParams.Ease))
                .Join(restartButton.transform
                    .DOScale(Vector3.zero, buttonsHideParams.Duration)
                    .SetEase(buttonsHideParams.Ease))
                .OnComplete(disableAction);
        }

        private void Disable()
        {
            pausePanel.gameObject.SetActive(false);
        }

        private void OnRestartButtonClick()
        {
            SceneC.Instance.GameLoopC.Restart();
        }
    }
}
