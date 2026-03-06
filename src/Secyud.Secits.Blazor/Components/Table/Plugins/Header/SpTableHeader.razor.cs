using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.Plugins;

public partial class SpTableHeader<TItem> : ISpTableHeader<TItem>
{
    public override string PluginName => "table-header";

    [Parameter] public bool EnableResize { get; set; }
    [Parameter] public bool FitNextColumn { get; set; }

    private STable<TItem>? _table;
    private ISpTableColumn<TItem>? _currentDragColumn;

    protected override void OnComponentSet()
    {
        base.OnComponentSet();
        SetComponentRef(ref _table);
    }

    private Task SetDragAsync(ISpTableColumn<TItem>? dragColumn)
    {
        _currentDragColumn = dragColumn;
        return Task.CompletedTask;
    }
    
    private async Task OnDragColumnHeader(MouseEventArgs args)
    {
        // TODO
        if (_currentDragColumn is null || _table is null) return;
        var info = _currentDragColumn.ColumnInfo;
        if (info.Width is null)
        {
            _currentDragColumn = null;
            return;
        }

        info.Width = Math.Clamp(info.Width.Value + args.MovementX, info.MinWidth, info.MaxWidth);
        _table.SetDirty();
        await _table.RefreshAsync();
    }
}