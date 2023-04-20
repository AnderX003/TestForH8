using System;
using System.Collections.Generic;
using GameplayLogic.Cells;
using SceneManagement;
using UnityEngine;

namespace GameplayLogic.PlacedRectangles
{
    [Serializable]
    public class RectanglesPlacer
    {
        [SerializeField] private float showInterval;
        [SerializeField] private float hideInterval;
        [SerializeField] private RectanglesColors colors;

        private List<PlacedRectangle> rectangles;
        private GameGrid grid;

        public void Init()
        {
            grid = SceneC.Instance.GameGrid;
            rectangles = new List<PlacedRectangle>(grid.GridRectanglesCount);
            colors.Init();
        }

        public void Place(Cell startCell, Cell currentCell)
        {
            var color = colors.Next();
            var rectangle = new PlacedRectangle(grid, startCell, currentCell, color);
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
