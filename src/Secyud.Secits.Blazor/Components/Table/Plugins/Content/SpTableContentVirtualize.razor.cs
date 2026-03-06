using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace Secyud.Secits.Blazor.Plugins;

public partial class SpTableContentVirtualize<TItem> : ISpTableContent<TItem>
{
    public override string PluginName => "table-content-virtualize";

    [Parameter] public STablePosition Position { get; set; }
    [Parameter] public Func<DataRequest<TItem>, Task<DataResult<TItem>>>? Items { get; set; }

    [Parameter] public Func<Exception, Task>? ErrorHandler { get; set; }

    private Virtualize<TItem>? _virtualize;

    public List<TItem>? GetCurrentItems()
    {
        return _virtualize?.Items?.ToList();
    }

    protected override void GenerateClassList(List<string?> list)
    {
        base.GenerateClassList(list);
        list.Add("s-virtualize");
    }

    protected async ValueTask<ItemsProviderResult<TItem>> RequestItemAsync(ItemsProviderRequest request)
    {
        if (Items is null || Table is null)
        {
            return new ItemsProviderResult<TItem>([], 0);
        }

        var dataRequest = new DataRequest<TItem>
        {
            SkipCount = request.StartIndex,
            PageSize = request.Count,
            DataFields = Table.GetDataFields(),
        };
        var result = await Items(dataRequest);
        return new ItemsProviderResult<TItem>(result.Items, result.TotalCount);
    }

    public async Task RefreshContentAsync()
    {
        if (_virtualize is null) return;
        await _virtualize.RefreshDataAsync();
    }
}