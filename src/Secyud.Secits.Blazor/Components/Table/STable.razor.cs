using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Plugins;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class STable<TItem> : IThemedComponent, IPluggableComponent
{
    public STable()
    {
        ColumnContext = new SpTableColumnContext<TItem>
        {
            Table = this
        };
        PluginContext = new SPluginContext(this);
    }

    protected override string ComponentClass => "s-table";
    [Parameter] public SColor Color { get; set; }
    [Parameter] public RenderFragment? Columns { get; set; }
    protected SpTableColumnContext<TItem> ColumnContext { get; }

    public List<DataField> GetDataFields()
    {
        return ColumnContext.Columns
            .Select(u => u.DataField)
            .ToList();
    }

    public List<ISpTableColumn<TItem>> GetColumns()
    {
        return ColumnContext.Columns.ToList();
    }

    [Parameter] public RenderFragment? Plugins { get; set; }
    protected SPluginContext PluginContext { get; }
    protected SPluginContainer<ISpTableContent<TItem>> Content { get; } = new();
    protected SPluginsContainer<ISpTableHeader<TItem>> Header { get; } = new();
    protected SPluginsContainer<ISpTableFooter<TItem>> Footer { get; } = new();
    protected SPluginsContainer<ISpTableElement> Element { get; } = new();

    public void ApplyPlugin(ISPlugin plugin)
    {
        Content.TryApply(plugin);
        Header.TryApply(plugin);
        Footer.TryApply(plugin);
        Element.TryApply(plugin);
    }

    public void ForgoPlugin(ISPlugin plugin)
    {
        Content.TryForgo(plugin);
        Header.TryForgo(plugin);
        Footer.TryForgo(plugin);
        Element.TryForgo(plugin);
    }

    public Task RefreshAsync()
    {
        return InvokeAsync(StateHasChanged);
    }

    #region Selection

    [Parameter] public Func<TItem, object>? Key { get; set; }

    public object? GetKeyOrDefault(TItem? item)
    {
        if (item is not null)
        {
            return GetKey(item);
        }

        return null;
    }

    public object GetKey(TItem item) => Key?.Invoke(item) ?? item!;

    private TItem? _selectedItem;
    private object? _selectedKey;

    [Parameter]
    public TItem? SelectedItem
    {
        get;
        set
        {
            if (Equals(field, value)) return;
            field = value;
            _selectedItem = value;
            _selectedKey = GetKeyOrDefault(value);
        }
    }

    [Parameter] public EventCallback<TItem?> SelectedItemChanged { get; set; }

    public void SetSelectedItem(TItem? item)
    {
        _selectedItem = item;
        _selectedKey = GetKeyOrDefault(item);
        SelectedItemChanged.InvokeAsync(_selectedItem).ConfigureAwait(false);
    }

    public TItem? GetSelectedItem() => _selectedItem;
    public object? GetSelectedKey() => _selectedKey;

    private List<TItem>? _selectedItems;
    private HashSet<object> _selectedKeys = [];

    [Parameter]
    public List<TItem>? SelectedItems
    {
        get;
        set
        {
            field = value;
            _selectedItems = value;
            if (value is not null)
            {
                _selectedKeys.Clear();
                _selectedKeys.UnionWith(value.Select(GetKey));
            }
        }
    }

    [Parameter] public EventCallback<List<TItem>?> SelectedItemsChanged { get; set; }

    public void SetSelectedItems(List<TItem>? items)
    {
        _selectedItems = items;
        _selectedKeys.Clear();
        if (items is not null)
        {
            _selectedKeys.UnionWith(items.Select(GetKey));
        }

        SelectedItemsChanged.InvokeAsync(_selectedItems).ConfigureAwait(false);
    }

    public List<TItem>? GetSelectedItems() => _selectedItems;
    public HashSet<object> GetSelectedKeys() => _selectedKeys;

    public List<TItem>? GetCurrentItems()
    {
        return Content.Get()?.GetCurrentItems();
    }

    #endregion
}