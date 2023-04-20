using GameplayObjects;
using GameplayObjects.PreviewRectangle;
using Helpers;
using LevelsManagement;
using SceneManagement.Pools;
using TouchLogic;
using UI;
using UnityEngine;

namespace SceneManagement
{
    public class SceneC : MonoSingleton<SceneC>
    {
        [field: SerializeField] public UIHolder UIHolder { get; private set; }
        [field: SerializeField] public PoolsHolder PoolsHolder { get; private set; }
        [field: SerializeField] public CellsFinder CellsFinder { get; private set; }
        [field: SerializeField] public CameraC CameraC { get; private set; }
        [field: SerializeField] public GameGrid GameGrid { get; private set; }

        [SerializeField] private RectanglePreview rectanglePreview;
        [SerializeField] private TouchHandler touchHandler;

        public GameLoopC GameLoopC { get; private set; }

        private void Start()
        {
            GameLoopC = new GameLoopC();

            GameLoopC.Init();
            UIHolder.Init();
            PoolsHolder.Init();
            CellsFinder.Init();
            CameraC.Init();
            rectanglePreview.Init();

            var levelParams = LevelsC.Instance.GetLevelParams();
            GameGrid.Init(levelParams);

            var placingChecker = new RectanglePlacingChecker();
            placingChecker.Init(GameGrid);
            touchHandler.Init(rectanglePreview, placingChecker);
        }

        private void Update()
        {
            touchHandler.Update();
        }
    }
}
