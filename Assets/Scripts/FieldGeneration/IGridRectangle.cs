using System.Collections.Generic;
using UnityEngine;

namespace FieldGeneration
{
    public interface IGridRectangle
    {
        IGridCell MainCell { get; }
        IEnumerable<IGridCell> Cells { get; }
        Vector2Int RootCoord { get; }
        Vector2Int Size { get; }
    }
}
