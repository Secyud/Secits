namespace Secyud.Secits.Blazor;

public class SSelection<TItem>(ISelectionComponent<TItem> component)
{
    public TItem? SelectedItem { get; private set; }
    public object? SelectedKey { get; private set; }
    public List<TItem>? SelectedItems { get; private set; }
    public HashSet<object> SelectedKeys { get; } = [];

    public void SetSelectedItem(TItem? selectedItem)
    {
        SelectedItem = selectedItem;
        SelectedKey = component.GetKeyOrDefault(selectedItem);
    }

    public void SetSelectedItems(List<TItem>? selectedItems)
    {
        SelectedItems = selectedItems;
        if (selectedItems is not null)
        {
            SelectedKeys.Clear();
            SelectedKeys.UnionWith(selectedItems.Select(component.GetKey));
        }
    }

    public void SyncParameters(TItem? selectedItem, List<TItem>? selectedItems)
    {
        SetSelectedItem(selectedItem);
        SetSelectedItems(selectedItems);
    }
}