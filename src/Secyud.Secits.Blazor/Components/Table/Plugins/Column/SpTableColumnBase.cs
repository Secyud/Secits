using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

public abstract class SpTableColumnBase<TItem> : SPluginBase
{
    protected SpTableColumnBase()
    {
        ColumnInfo = new SpTableColumnInfo();
        DataField = new DataField(GetFiledName);
    }

    [CascadingParameter]
    protected SpTableColumnContext<TItem>? ColumnContext
    {
        get;
        set
        {
            if (field == value) return;
            field?.Columns.TryForgo(this);
            field = value;
            field?.Columns.TryApply(this);
            Context?.InvokeAsync(Context.StateHasChanged);
        }
    }

    public DataField DataField { get; }
    public SpTableColumnInfo ColumnInfo { get; }
    protected abstract string? GetFiledName();
    public RenderFragment GenerateCaption() => BuildRenderTree;
}