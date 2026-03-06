using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

public partial class SpTableContentPaged<TItem> : ISpTableContent<TItem>, ISpTableElement
{
    public override string PluginName => "table-content-paged";

    [Parameter] public STablePosition Position { get; set; }
    [Parameter] public Func<DataRequest<TItem>, Task<DataResult<TItem>>>? Items { get; set; }

    [Parameter] public Func<Exception, Task>? ErrorHandler { get; set; }


    protected DataResult<TItem>? Result { get; set; }

    protected int PageIndex
    {
        get;
        set
        {
            if (value == field) return;
            field = value;
            RefreshContentAsync().ConfigureAwait(false);
        }
    }

    protected int PageSize
    {
        get;
        set
        {
            if (value == field) return;
            field = value;
            RefreshContentAsync().ConfigureAwait(false);
        }
    } = 10;


    public List<TItem>? GetCurrentItems()
    {
        return Result?.Items.ToList();
    }

    public async Task RefreshContentAsync()
    {
        try
        {
            if (Items is null || Table is null) return;
            var request = new DataRequest<TItem>
            {
                PageSize = PageSize,
                SkipCount = PageSize * PageIndex,
                DataFields = Table.GetDataFields()
            };

            Result = await Items(request);
            await Table.RefreshAsync();
        }
        catch (Exception e)
        {
            if (ErrorHandler is null) throw;
            await ErrorHandler.Invoke(e);
        }
    }
}