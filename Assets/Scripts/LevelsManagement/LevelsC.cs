using Helpers;
using UnityEngine;

namespace LevelsManagement
{
    public class LevelsC : MonoSingleton<LevelsC>
    {
        [SerializeField] private LevelsProgression levelsProgression;
        [SerializeField] private LevelsChanger levelsChanger;

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
            return levelsProgression.GetNextLevelParams();
        }

        public void LoadNextLevel()
        {
            levelsChanger.LoadNextLevel();
        }
    }
}
