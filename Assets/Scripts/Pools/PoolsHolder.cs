﻿using System;
using UnityEngine;

namespace Pools
{
    [Serializable]
    public class PoolsHolder
    {
        [field: SerializeField] public CellsPool CellsPool { get; private set; }
        [field: SerializeField] public PlacedCellsPool PlacedCellsPool { get; private set; }

        public void Init()
        {
            CellsPool.Init();
            PlacedCellsPool.Init();
        }
    }
}
