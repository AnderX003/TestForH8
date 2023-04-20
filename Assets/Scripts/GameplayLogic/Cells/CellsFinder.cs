using System;
using UnityEngine;

namespace GameplayLogic.Cells
{
    [Serializable]
    public class CellsFinder : BaseFinder<Collider, Cell>
    {
        public void Init()
        {
        }

        public bool TryFindCellByCollider(Collider collider, out Cell cell)
        {
            return GetByKey(collider, out cell);
        }
    }
}
