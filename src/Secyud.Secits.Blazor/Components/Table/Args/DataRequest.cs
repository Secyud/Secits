namespace Secyud.Secits.Blazor;

public class DataRequest
{
    public required int PageSize { get; set; } = 10;
    public required int SkipCount { get; set; }
    public required List<DataField> DataFields { get; set; }
}