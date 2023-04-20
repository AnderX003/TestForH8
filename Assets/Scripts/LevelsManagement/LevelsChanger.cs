using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelsManagement
{
    [Serializable]
    public class LevelsChanger
    {
        [SerializeField] private int mainSceneIndex = 1;

        public void Init()
        {
        }

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(mainSceneIndex);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(mainSceneIndex);
        }
    }
}
