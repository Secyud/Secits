using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableIteratorPager<TValue> : IContentRenderer
{
    [Parameter]
    public int[]? PageSizes { get; set; }

    [Parameter]
    public RendererPosition PagerPosition { get; set; }

    public RendererPosition GetLayoutPosition()
    {
        return PagerPosition;
    }

    protected override void ApplySetting()
    {
        base.ApplySetting();
        Master.Content.Apply(this);

        if (PageSizes is { Length: > 0 })
            Master.DataRequest.PageSize = PageSizes[0];

        RefreshAsync(true).ConfigureAwait(false);
    }

    protected override void ForgoSetting()
    {
        base.ForgoSetting();
        Master.Content.Forgo(this);
    }

    protected override Task PreRefreshAsync(bool resetState)
    {
        if (resetState)
        {
            Master.DataRequest.PageIndex = 0;
        }

        return Task.CompletedTask;
    }

    private async Task PageSizeChangedAsync(int pageSize)
    {
        Master.DataRequest.PageSize = pageSize;
        await Master.RefreshAsync(false);
    }

    private async Task PageIndexChangedAsync(int pageIndex)
    {
        Master.DataRequest.PageIndex = pageIndex;
        await Master.RefreshAsync(false);
    }
}