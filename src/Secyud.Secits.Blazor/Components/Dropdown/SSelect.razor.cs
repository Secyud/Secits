using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SSelect<TItem> : ISelectionComponent<TItem>
{
    public SSelect()
    {
        Selection = new SSelection<TItem>(this);
    }

    protected override string ComponentClass => "s-select";
    public SSelection<TItem> Selection { get; }
    [Parameter] public bool MultiSelect { get; set; }
    [Parameter] public List<TItem>? Items { get; set; }
    [Parameter] public RenderFragment<TItem?>? ContentTemplate { get; set; } 
    [Parameter] public RenderFragment<TItem>? SelectTemplate { get; set; } 
    [Parameter] public Func<TItem, object>? Key { get; set; }
    [Parameter] public TItem? SelectedItem { get; set; }
    [Parameter] public EventCallback<TItem?> SelectedItemChanged { get; set; }

    public async Task SetSelectedItem(TItem? item)
    {
        Selection.SetSelectedItem(item);
        await SelectedItemChanged.InvokeAsync(item);
        await InvokeAsync(StateHasChanged);
    }

    [Parameter] public List<TItem>? SelectedItems { get; set; }

    [Parameter] public EventCallback<List<TItem>?> SelectedItemsChanged { get; set; }

    public async Task SetSelectedItems(List<TItem>? items)
    {
        Selection.SetSelectedItems(items);
        await SelectedItemsChanged.InvokeAsync(items);
        await InvokeAsync(StateHasChanged);
    }
}