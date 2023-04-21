using System;
using Helpers;
using Pools;
using UnityEngine;

namespace LevelsManagement
{
    public class LevelsC : MonoSingleton<LevelsC>
    {
        public event Action OnUnloadingLevel;

        [SerializeField] private LevelsProgression levelsProgression;
        [SerializeField] private LevelsChanger levelsChanger;

        [field: SerializeField] public PoolsHolder PoolsHolder { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            PoolsHolder.Init();
            levelsProgression.Init();
            levelsChanger.Init();
            levelsChanger.LoadNextLevel();
        }

        public LevelParams GetLevelParams()
        {
            return levelsProgression.GetLevelParams();
        }

        public void LoadNextLevel()
        {
            OnUnloadingLevel?.Invoke();
            levelsProgression.CalculateNextGridSize();
            levelsChanger.LoadNextLevel();
        }

        public void RestartLevel()
        {
            OnUnloadingLevel?.Invoke();
            levelsChanger.RestartLevel();
        }
    }
}
