namespace FieldGeneration
{
    public interface IGridCell
    {
        int X { get; }
        int Y { get; }
        Rectangle Rectangle { get; }
    }
}
