using System;
using System.Collections.Generic;
using GameplayLogic.Cells;
using UnityEngine;

namespace GameplayLogic.PlacedRectangles
{
    [Serializable]
    public class RectanglesPlacer
    {
        [SerializeField] private float showInterval;
        [SerializeField] private float hideInterval;

        private List<PlacedRectangle> rectangles;
        private GameGrid gameGrid;

        public void Init(GameGrid gameGrid)
        {
            this.gameGrid = gameGrid;
            rectangles = new List<PlacedRectangle>(gameGrid.GridRectanglesCount);
        }

        public void Place(Cell startCell, Cell currentCell)
        {
            var rectangle = new PlacedRectangle(gameGrid, startCell, currentCell);
            rectangles.Add(rectangle);
            rectangle.Place(showInterval);
        }

        public void DeletePreview(PlacedRectangle rectangle)
        {
            rectangle.PunchScale();
        }

        public void Delete(PlacedRectangle rectangle)
        {
            rectangles.Remove(rectangle);
            rectangle.UnPlace(hideInterval);
        }
    }
}
