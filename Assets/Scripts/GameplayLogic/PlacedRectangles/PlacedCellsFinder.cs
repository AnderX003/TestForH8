using System;
using UnityEngine;

namespace GameplayLogic.PlacedRectangles
{
    [Serializable]
    public class PlacedCellsFinder : BaseFinder<Collider, PlacedCell>
    {
        public void Init()
        {
        }

        public bool TryFindCellByCollider(Collider collider, out PlacedCell cell)
        {
            return GetByKey(collider, out cell);
        }
    }
}
