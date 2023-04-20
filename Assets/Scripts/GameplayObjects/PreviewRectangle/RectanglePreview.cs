using UnityEngine;

namespace GameplayObjects.PreviewRectangle
{
    public class RectanglePreview : MonoBehaviour
    {
        [SerializeField] private RectanglePreviewSprings springs;
        [SerializeField] private RectanglePreviewCalculator calculator;
        [SerializeField] private RectanglePreviewColorVisualizer colorVisualizer;

        private Cell startCell;

        public void Init()
        {
            springs.Init();
        }

        public void Update()
        {
            springs.Update();
        }

        public void Show(Cell cell)
        {
            startCell = cell;
            colorVisualizer.ResetColor();
            springs.ResetOnShow();
            CalculateTransform(startCell);
            springs.SetStartPosition();
        }

        public void ChangeCell(Cell currentCell, bool canPlace)
        {
            colorVisualizer.SetMatchColor(canPlace);
            CalculateTransform(currentCell);
        }

        public void Hide()
        {
            springs.Hide();
            colorVisualizer.Hide();
        }

        private void CalculateTransform(Cell currentCell)
        {
            var targetPosition = calculator.CalculatePosition(startCell, currentCell);
            var targetScale = calculator.CalculateScale(startCell, currentCell);
            springs.SetTargetPosition(targetPosition);
            springs.SetTargetScale(targetScale);
        }
    }
}
