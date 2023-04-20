using UnityEngine;

namespace GridGeneration
{
    public interface IGridCell
    {
        int X { get; }
        int Y { get; }
        Vector2Int Coords { get; }
        bool IsMain { get; }
        Rectangle Rectangle { get; }
    }
}
