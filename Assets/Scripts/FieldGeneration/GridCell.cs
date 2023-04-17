namespace FieldGeneration
{
    public class GridCell : IGridCell
    {
        public int X { get; }
        public int Y { get; }
        public bool IsOccupied { get; private set; }
        public Rectangle Rectangle { get; private set; }

        public GridCell(int x, int y)
        {
            X = x;
            Y = y;
            IsOccupied = false;
        }

        public void AttachRectangle(Rectangle rectangle)
        {
            Rectangle = rectangle;
            IsOccupied = true;
        }
    }
}
