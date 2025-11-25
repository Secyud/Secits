namespace Secyud.Secits.Blazor;

public class DataResult<TValue>
{
    public int TotalCount { get; set; }
    public IEnumerable<TValue> Items { get; set; } = [];
}