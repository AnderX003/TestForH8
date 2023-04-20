using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayLogic
{
    [Serializable]
    public class CellsFinder
    {
        private Dictionary<Collider, Cell> cellsByCollider = new();

        public void Init()
        {
        }

        public void Register(Cell cell, Collider collider)
        {
            cellsByCollider.Add(collider, cell);
        }

        public void Unregister(Cell cell, Collider collider)
        {
            cellsByCollider.Remove(collider);
        }

        public bool TryFindCellByCollider(Collider collider, out Cell cell)
        {
            return cellsByCollider.TryGetValue(collider, out cell);
        }
    }
}
