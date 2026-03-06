namespace Secyud.Secits.Blazor.Plugins;

public partial class SpTableColumnSelection<TItem> : ISpTableColumn<TItem>
{
    public override string PluginName => "table-column-selection";
    private STable<TItem>? _table;

    protected override void OnComponentSet()
    {
        base.OnComponentSet();
        SetComponentRef(ref _table);
    }

    private bool AllItemSelected
    {
        get
        {
            if (_table?.GetCurrentItems() is not
                {
                    Count: > 0
                } items) return false;
            var keys = _table.GetSelectedKeys();
            return items.All(u => keys.Contains(_table.GetKey(u)));
        }
        set
        {
            if (_table is null) return;
            var items = (value ? _table.GetCurrentItems() : []) ?? [];
            var originItems = _table.GetSelectedItems() ?? [];
            var result = items.UnionBy(originItems, _table.GetKey);
            _table.SetSelectedItems(result.ToList());
        }
    }

    protected void OnCheckChanged(TItem item, bool b)
    {
        if (_table is null) return;
        var items = _table.GetSelectedItems() ?? [];
        var key = _table.GetKey(item);
        if (b)
        {
            items.Add(item);
        }
        else
        {
            items.RemoveAll(u => _table.GetKey(u) == key);
        }

        _table.SetSelectedItems(items);
    }

    protected override string? GetFiledName()
    {
        return null;
    }

    public object? GetField(TItem item)
    {
        return _table?.GetKey(item);
    }
}