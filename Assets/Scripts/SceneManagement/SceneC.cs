using FieldGeneration;
using Helpers;
using LevelsManagement;
using UI;
using UnityEngine;

namespace SceneManagement
{
    public class SceneC : MonoSingleton<SceneC>
    {
        [field: SerializeField] public UIHolder UIHolder { get; private set; }

        public GameLoopC GameLoopC { get; private set; }

        private void Start()
        {
            GameLoopC = new GameLoopC();

            GameLoopC.Init();
            UIHolder.Init();
            
            var levelParams = LevelsC.Instance.GetLevelParams();
            var field = new Field(levelParams.GridSize);
            field.DivideToRectangles(levelParams.GenerationLimits);

            Debug.Log(field.ToString());
        }
    }
}
