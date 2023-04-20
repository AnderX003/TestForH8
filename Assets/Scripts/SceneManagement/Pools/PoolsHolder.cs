using System;
using UnityEngine;

namespace SceneManagement.Pools
{
    [Serializable]
    public class PoolsHolder
    {
        [field: SerializeField] public CellsPool CellsPool { get; private set; }

        public void Init()
        {
            CellsPool.Init();
        }
    }
}
