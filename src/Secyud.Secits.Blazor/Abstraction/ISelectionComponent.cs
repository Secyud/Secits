using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface ISelectionComponent<TItem>
{
    Func<TItem, object>? Key { get; }
    TItem? SelectedItem { get; }
    EventCallback<TItem?> SelectedItemChanged { get; }
    List<TItem>? SelectedItems { get; }
    EventCallback<List<TItem>?> SelectedItemsChanged { get; }
    SSelection<TItem> Selection { get; }
}