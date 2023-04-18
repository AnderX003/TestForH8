using System;
using LevelsManagement;
using UnityEngine;

namespace SceneManagement
{
    public class GameLoopC
    {
        public event Action OnPause;
        public event Action OnResume;
        public event Action OnWin;

        public void Init()
        {
            Time.timeScale = 1f;
        }

        public void Win()
        {
            Time.timeScale = 0f;
            OnWin?.Invoke();
        }

        public void NextLevel()
        {
            LevelsC.Instance.LoadNextLevel();
        }

        public void Restart()
        {
            LevelsC.Instance.RestartLevel();
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            OnPause?.Invoke();
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            OnResume?.Invoke();
        }
    }
}
