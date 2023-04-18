using FieldGeneration;
using Helpers;
using LevelsManagement;
using UnityEngine;

public class SceneC : MonoSingleton<SceneC>
{
    private void Start()
    {
        var levelParams = LevelsC.Instance.GetLevelParams();
        var field = new Field(levelParams.GridSize);
        field.DivideToRectangles(levelParams.GenerationLimits);
        Debug.Log(field.ToString());
    }
}