using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor.Element;

public partial class SPager
{
    [Inject]
    private IIconProvider IconProvider { get; set; } = null!;

    [Parameter]
    public int MaxPageCount { get; set; }

    [Parameter]
    public int NeighbourCount { get; set; } 

    [Parameter]
    public int PageIndex { get; set; }

    [Parameter]
    public EventCallback<int> PageIndexChanged { get; set; }

    protected async Task ChangePageIndexAsync(int index)
    {
        if (index < 0 || index >= MaxPageCount || PageIndex == index)
            return;

        await PageIndexChanged.InvokeAsync(index);


        PageIndex = index;
        await InvokeAsync(StateHasChanged);
    }

    protected async Task TurnToFirstPageAsync()
    {
        if (PageIndex != 0) await ChangePageIndexAsync(0);
    }

    protected async Task TurnToPreviewPageAsync()
    {
        if (PageIndex > 0) await ChangePageIndexAsync(PageIndex - 1);
    }

    protected async Task TurnToNextPageAsync()
    {
        if (PageIndex < MaxPageCount - 1) await ChangePageIndexAsync(PageIndex + 1);
    }

    protected async Task TurnToLastPageAsync()
    {
        if (PageIndex != MaxPageCount - 1) await ChangePageIndexAsync(MaxPageCount - 1);
    }

    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-pager", Class);
    }

    protected RenderFragment GeneratePageButtons(int min, int max) =>
        builder =>
        {
            for (int i = min; i < max; i++)
            {
                builder.AddContent(i, GeneratePageButton(i));
            }
        };
}