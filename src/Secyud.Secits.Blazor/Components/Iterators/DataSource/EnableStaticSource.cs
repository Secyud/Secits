using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class EnableStaticSource<TValue> : SPluginBase<SIteratorBase<TValue>>, IDataSourceProvider<TValue>
{
    [Parameter]
    public IReadOnlyList<TValue>? Items { get; set; }

    protected override void ApplySetting()
    {
        Master.DataSource.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.DataSource.Forgo(this);
    }

    protected virtual Task<IEnumerable<TValue>> ApplyDataHandleAsync(IEnumerable<TValue> items, DataRequest request)
    {
        return Task.FromResult(items);
    }

    public async Task<DataResult<TValue>> GetDataAsync(DataRequest request)
    {
        var res = new DataResult<TValue>();
        if (Items is null) return res;
        var items = (await ApplyDataHandleAsync(Items, request)).ToList();
        res.TotalCount = items.Count;
        res.Items = items.Skip(request.SkipCount).Take(request.PageSize).ToList();
        return res;
    }
}