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
            OnPause?.Invoke();
        }

        public void Resume()
        {
            OnResume?.Invoke();
        }
    }
}
