namespace Secyud.Secits.Blazor;

public class DataResult<TValue>
{
    public int TotalCount { get; set; }
    public IReadOnlyList<TValue> Items { get; set; } = [];
}