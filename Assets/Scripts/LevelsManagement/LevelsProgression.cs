using System;
using GridGeneration;
using UnityEngine;

namespace LevelsManagement
{
    [Serializable]
    public class LevelsProgression
    {
        [SerializeField] private RectangleGenerationLimits generationLimits;
        [SerializeField] private Vector2Int minGridSize;
        [SerializeField] private Vector2Int maxGridSize;
        private Vector2Int gridSize;

        public void Init()
        {
            gridSize = minGridSize;
        }

        public LevelParams GetLevelParams()
        {
            return new LevelParams
            {
                GridSize = gridSize,
                GenerationLimits = generationLimits
            };
        }

        public void CalculateNextGridSize()
        {
            if (gridSize.x <= maxGridSize.x && gridSize.y <= maxGridSize.y)
            {
                if (gridSize.x >= gridSize.y)
                {
                    gridSize.x++;
                }
                else
                {
                    gridSize.y++;
                }
            }
        }
    }
}
