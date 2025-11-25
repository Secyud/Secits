namespace Secyud.Secits.Blazor;

public class GridColumnListContext<TValue>
{
    private readonly List<GridColumnContext<TValue>> _columns = [];
    private int _width;

    public IReadOnlyList<GridColumnContext<TValue>> Columns => _columns;

    public int Count => _columns.Count;
    public int MaxSequence { get; set; }
    public int MinSequence { get; set; }

    public int Width => _width;

    public void AddColumn(GridColumnContext<TValue> column)
    {
        column.Index = _columns.Count;
        _columns.Add(column);
        MaxSequence = Math.Max(MaxSequence, column.Sequence);
        MinSequence = Math.Min(MinSequence, column.Sequence);
        _width += column.Renderer.RealWidth;
    }
}