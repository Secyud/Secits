using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableIteratorFixed<TValue> : IIteratorRenderer<TValue>
{
    protected List<TValue> Items { get; private set; } = [];
    protected int TotalCount { get; private set; } = 1000;

    protected override void ApplySetting()
    {
        Master.ItemsRenderer.Apply(this);
        RefreshAsync(true).ConfigureAwait(false);
    }

    protected override void ForgoSetting()
    {
        Master.ItemsRenderer.Forgo(this);
    }

    public async Task RefreshAsync(bool resetState)
    {
        await PreRefreshAsync(resetState);
        await OnRefreshAsync(resetState);
        await PostRefreshAsync(resetState);
    }

    protected virtual Task PreRefreshAsync(bool resetState)
    {
        Master.DataRequest.PageSize = TotalCount;
        return Task.CompletedTask;
    }

    protected virtual async Task OnRefreshAsync(bool resetState)
    {
        if (Master.DataSource.Get() is not { } source) return;
        var result = await source.GetDataAsync(Master.DataRequest);
        Items = result.Items.ToList();
        TotalCount = result.TotalCount;
    }

    protected virtual async Task PostRefreshAsync(bool resetState)
    {
        await InvokeAsync(StateHasChanged);
    }
}