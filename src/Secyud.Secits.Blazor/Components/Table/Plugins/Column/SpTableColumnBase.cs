using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

public abstract class SpTableColumnBase : SPluginBase
{
    protected SpTableColumnBase()
    {
        ColumnInfo = new SpTableColumnInfo();
        DataField = new DataField(GetFiledName);
    }

    public DataField DataField { get; }
    public SpTableColumnInfo ColumnInfo { get; }
    protected abstract string? GetFiledName();
    public RenderFragment GenerateCaption() => BuildRenderTree;
}