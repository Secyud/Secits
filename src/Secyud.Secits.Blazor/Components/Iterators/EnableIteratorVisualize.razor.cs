using Microsoft.AspNetCore.Components.Web.Virtualization;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableIteratorVisualize<TValue> : IIteratorRenderer<TValue>, IExtendClassStyleBuilder
{
    private Virtualize<TValue>? _virtualize;

    protected override void ApplySetting()
    {
        Master.ItemsRenderer.Apply(this);
        Master.ClassStyleBuilders.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.ItemsRenderer.Forgo(this);
        Master.ClassStyleBuilders.Forgo(this);
    }

    public async ValueTask<ItemsProviderResult<TValue>> RefreshRowsAsync(ItemsProviderRequest request)
    {
        if (Master.DataSource.Get() is not { } source) return default;
        Master.DataRequest.SkipCount = request.StartIndex;
        Master.DataRequest.PageSize = request.Count;
        Master.DataRequest.CancellationToken = request.CancellationToken;
        var result = await source.GetDataAsync(Master.DataRequest);
        return new ItemsProviderResult<TValue>(result.Items, result.TotalCount);
    }

    public async Task RefreshAsync(bool resetState)
    {
        if (_virtualize is not null)
        {
            await _virtualize.RefreshDataAsync();
        }
    }

    public void BuildExtendClassStyle(ClassStyleContext context)
    {
        context.AppendClass("visualize");
    }
}