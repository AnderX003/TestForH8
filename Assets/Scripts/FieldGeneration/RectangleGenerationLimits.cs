using UnityEngine;

namespace FieldGeneration
{
    [CreateAssetMenu(fileName = "rectangleGenerationLimits", menuName = "SO/Rectangle Generation Limits")]
    public class RectangleGenerationLimits : ScriptableObject
    {
        [field: SerializeField] public int minRectangleArea { get; private set; } = 2;
        [field: SerializeField] public int maxRectangleArea { get; private set; } = 8;
        [field: SerializeField] public int maxRectangleLength { get; private set; } = 4;
    }
}
