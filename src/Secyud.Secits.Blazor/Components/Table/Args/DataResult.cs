namespace Secyud.Secits.Blazor;

public class DataResult<TValue>(IReadOnlyList<TValue> items, int totalCount)
{
    public DataResult() : this([], 0)
    {
    }

    public int TotalCount { get; set; } = totalCount;
    public IReadOnlyList<TValue> Items { get; set; } = items;
}