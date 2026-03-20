using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SPager : IThemedComponent, ISizedComponent
{
    protected override string ComponentClass => "s-pager";

    private int _pageIndex;

    [Parameter] public int MaxPageCount { get; set; }
    [Parameter] public int ShowPageCount { get; set; } = 10;
    [Parameter] public int PageIndex { get; set; }
    [Parameter] public EventCallback<int> PageIndexChanged { get; set; }
    [Parameter] public SColor Color { get; set; }
    [Parameter] public SSize Size { get; set; }

    protected override void OnParametersSet()
    {
        _pageIndex = PageIndex;
    }

    protected async Task ChangePageIndexAsync(int index)
    {
        if (index < 0 || index >= MaxPageCount || _pageIndex == index)
            return;

        await PageIndexChanged.InvokeAsync(index);

        _pageIndex = index;

        await InvokeAsync(StateHasChanged);
    }

    protected async Task TurnToFirstPageAsync()
    {
        if (_pageIndex != 0)
            await ChangePageIndexAsync(0);
    }

    protected async Task TurnToPreviewPageAsync()
    {
        if (_pageIndex > 0)
            await ChangePageIndexAsync(PageIndex - 1);
    }

    protected async Task TurnToNextPageAsync()
    {
        if (_pageIndex < MaxPageCount - 1)
            await ChangePageIndexAsync(PageIndex + 1);
    }

    protected async Task TurnToLastPageAsync()
    {
        if (_pageIndex != MaxPageCount - 1)
            await ChangePageIndexAsync(MaxPageCount - 1);
    }
}