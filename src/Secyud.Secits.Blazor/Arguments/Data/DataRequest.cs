namespace Secyud.Secits.Blazor;

public class DataRequest
{
    private int _pageIndex;
    public static int DefaultPageSize { get; set; } = 10;

    public int PageIndex
    {
        get => _pageIndex;
        set
        {
            _pageIndex = value;
            SkipCount = _pageIndex * PageSize;
        }
    }

    public int PageSize { get; set; } = DefaultPageSize;
    public int SkipCount { get; set; }

    public CancellationToken CancellationToken { get; set; }

    public List<DataSorter> Sorters { get; } = [];

    public List<DataFilter> Filters { get; } = [];
}