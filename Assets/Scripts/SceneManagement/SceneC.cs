﻿using GameplayLogic;
using GameplayLogic.Cells;
using GameplayLogic.PlacedRectangles;
using GameplayLogic.PreviewRectangle;
using Helpers;
using LevelsManagement;
using TouchLogic;
using UI;
using UnityEngine;

namespace SceneManagement
{
    public class SceneC : MonoSingleton<SceneC>
    {
        [field: SerializeField] public UIHolder UIHolder { get; private set; }
        [field: SerializeField] public CellsFinder CellsFinder { get; private set; }
        [field: SerializeField] public PlacedCellsFinder PlacedCellsFinder { get; private set; }
        [field: SerializeField] public RectanglesPlacer RectanglesPlacer { get; private set; }
        [field: SerializeField] public CameraC CameraC { get; private set; }
        [field: SerializeField] public GameGrid GameGrid { get; private set; }

        [SerializeField] private RectanglePreview rectanglePreview;
        [SerializeField] private UserActionHandler userActionHandler;

        public GameLoopC GameLoopC { get; private set; }

        private void Start()
        {
            GameLoopC = new GameLoopC();

            GameLoopC.Init();
            UIHolder.Init();
            CellsFinder.Init();
            PlacedCellsFinder.Init();
            CameraC.Init();
            rectanglePreview.Init();

            var levelParams = LevelsC.Instance.GetLevelParams();
            GameGrid.Init(levelParams);

            var placingChecker = new RectanglePlacingChecker();
            placingChecker.Init();
            RectanglesPlacer.Init();
            userActionHandler.Init(rectanglePreview, placingChecker);
        }

        private void Update()
        {
            userActionHandler.Update();
        }
    }
}
