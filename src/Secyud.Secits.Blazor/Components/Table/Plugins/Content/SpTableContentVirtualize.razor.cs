using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor.Plugins;

public partial class SpTableContentVirtualize<TItem> : ISpTableContent<TItem>, ISpTableStyle
{
    private List<TItem>? _items;
    public override string PluginName => "table-content-virtualize";

    [Parameter] public STablePosition Position { get; set; }
    [Parameter] public Func<DataRequest, Task<DataResult<TItem>>>? Items { get; set; }
    [Parameter] public Func<Exception, Task>? ErrorHandler { get; set; }

    [Parameter]
    public int Height
    {
        get;
        set => SetDirty(ref field, value);
    } = 800;

    private Virtualize<TItem>? _virtualize;

    public List<TItem>? GetCurrentItems()
    {
        return _items;
    }

    public void BuildClassStyle(ClassStyleContext context)
    {
        context.AppendClass("s-virtualize");
        context.AppendStyle("height", $"{Height}px");
    }

    protected async ValueTask<ItemsProviderResult<TItem>> RequestItemAsync(ItemsProviderRequest request)
    {
        if (Items is null || Table is null)
        {
            return new ItemsProviderResult<TItem>([], 0);
        }

        var dataRequest = new DataRequest
        {
            SkipCount = request.StartIndex,
            PageSize = request.Count,
            DataFields = Table.GetDataFields(),
        };
        var result = await Items(dataRequest);
        _items = result.Items.ToList();
        return new ItemsProviderResult<TItem>(result.Items, result.TotalCount);
    }

    public async Task RefreshContentAsync()
    {
        if (_virtualize is null) return;
        await _virtualize.RefreshDataAsync();
    }
}