﻿using System;
using FieldGeneration;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public LevelParams GetNextLevelParams()
        {
            return new LevelParams
            {
                GridSize = CalculateNextGridSize(),
                GenerationLimits = generationLimits
            };
        }

        public Vector2Int CalculateNextGridSize()
        {
            var result = gridSize;
            if (gridSize.x <= maxGridSize.x && gridSize.y <= maxGridSize.y)
            {
                if (gridSize.x < gridSize.y)
                {
                    gridSize.x++;
                }
                else
                {
                    gridSize.y++;
                }
            }
            return result;
        }
    }
}
