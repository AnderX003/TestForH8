using System.Collections.Generic;
using UnityEngine;

namespace GridGeneration
{
    public interface IGridRectangle
    {
        IGridCell MainCell { get; }
        IEnumerable<IGridCell> Cells { get; }
        int CellsAmount { get; }
        Vector2Int RootCoord { get; }
        Vector2Int Size { get; }
    }
}
