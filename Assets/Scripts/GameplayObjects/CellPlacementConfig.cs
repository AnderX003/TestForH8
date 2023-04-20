using UnityEngine;

namespace GameplayObjects
{
    [CreateAssetMenu(menuName = "SO/Cell Placement Config", fileName = "cellPlacementConfig")]
    public class CellPlacementConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2 Step { get; private set; }
        [field: SerializeField] public Vector2 StartPos { get; private set; }
    }
}
