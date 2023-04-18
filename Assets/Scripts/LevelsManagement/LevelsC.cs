using Helpers;
using UnityEngine;

namespace LevelsManagement
{
    public class LevelsC : MonoSingleton<LevelsC>
    {
        [SerializeField] private LevelsProgression levelsProgression;
        [SerializeField] private LevelsChanger levelsChanger;
        private bool restarting;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            levelsProgression.Init();
            levelsChanger.Init();
            levelsChanger.LoadNextLevel();
        }

        public LevelParams GetLevelParams()
        {
            return restarting 
                ? levelsProgression.GetLevelParams()
                : levelsProgression.GetNextLevelParams();
        }

        public void LoadNextLevel()
        {
            restarting = false;
            levelsChanger.LoadNextLevel();
        }

        public void RestartLevel()
        {
            restarting = true;
            levelsChanger.RestartLevel();
        }
    }
}
