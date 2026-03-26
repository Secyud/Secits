using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor.Plugins;

public partial class SpTableContent<TItem> : ISpTableContent<TItem>, ISpTableStyle
{
    public override string PluginName => "table-content";
    [Parameter] public List<TItem>? Items { get; set; }

    [Parameter]
    public bool Scrollable
    {
        get;
        set => SetDirty(ref field, value);
    } = true;

    public void BuildClassStyle(ClassStyleContext context)
    {
        if (Scrollable)
        {
            context.AppendClass("s-virtualize");
        }
    }

    public List<TItem>? GetCurrentItems()
    {
        return Items;
    }
}