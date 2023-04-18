using System;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class WinUI
    {
        [SerializeField] private GameObject WinPanel;
        [SerializeField] private Button nextLevelButton;

        public void Init()
        {
            SceneC.Instance.GameLoopC.OnWin += OnWin;
            nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
        }

        private void OnWin()
        {
            WinPanel.SetActive(true);
        }

        private void OnNextLevelButtonClick()
        {
            SceneC.Instance.GameLoopC.NextLevel();
        }
    }
}
