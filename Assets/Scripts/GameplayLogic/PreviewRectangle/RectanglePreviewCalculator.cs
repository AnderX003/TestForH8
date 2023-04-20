using System;
using GameplayLogic.Cells;
using Helpers.ExtensionsAndDS;
using UnityEngine;

namespace GameplayLogic.PreviewRectangle
{
    [Serializable]
    public class RectanglePreviewCalculator
    {
        [SerializeField] private float cellWidth;
        [SerializeField] private float cellStep;
        [SerializeField] private float scaleMultiplier;

        public Vector3 CalculatePosition(Cell startCell, Cell currentCell)
        {
            if (currentCell == startCell)
            {
                return startCell.GetPosition().WithY(0f);
            }
            else
            {
                return
                    (startCell.GetPosition() + currentCell.GetPosition()).WithY(0f) / 2f;
            }
        }

        public Vector2 CalculateScale(Cell startCell, Cell currentCell)
        {
            if (currentCell == startCell)
            {
                return scaleMultiplier * new Vector2(cellWidth, cellWidth);
            }
            else
            {
                Vector2 margin = startCell.GetGridCoords() - currentCell.GetGridCoords();
                margin = margin.Abs();
                return scaleMultiplier * new Vector2(
                    (margin.y + 1) * cellWidth + margin.y * cellStep,
                    (margin.x + 1) * cellWidth + margin.x * cellStep);
            }
        }
    }
}
