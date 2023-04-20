using System;
using DG.Tweening;
using Helpers.ExtensionsAndDS;
using SceneManagement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

namespace UI
{
    [Serializable]
    public class WinUI
    {
        [SerializeField] private Image winPanel;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Transform youWinText;
        [SerializeField] private float showDelay;
        [SerializeField] private DoAnimationParams<Ease> panelShowParams;
        [SerializeField] private DoAnimationParams<Ease> textShowParams;
        [SerializeField] private DoAnimationParams<Ease> buttonShowParams;

        public void Init()
        {
            SceneC.Instance.GameLoopC.OnWin += OnWin;
            nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
        }

        private void OnWin()
        {
            winPanel.gameObject.SetActive(true);

            var targetColor = winPanel.color;
            winPanel.color = winPanel.color.WithAlpha(0f);
            youWinText.localScale = Vector3.zero;
            nextLevelButton.transform.localScale = Vector3.zero;

            DOTween.Sequence()
                .SetDelay(showDelay)
                .Append(winPanel
                    .DOFade(targetColor.a, panelShowParams.Duration)
                    .SetEase(panelShowParams.Ease))
                .Append(youWinText
                    .DOScale(Vector3.one, textShowParams.Duration)
                    .SetEase(textShowParams.Ease))
                .Append(nextLevelButton.transform
                    .DOScale(Vector3.one, buttonShowParams.Duration)
                    .SetEase(buttonShowParams.Ease));
        }

        private void OnNextLevelButtonClick()
        {
            SceneC.Instance.GameLoopC.NextLevel();
        }
    }
}
