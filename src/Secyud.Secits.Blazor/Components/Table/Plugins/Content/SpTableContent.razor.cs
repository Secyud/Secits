using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

public partial class SpTableContent<TItem> : ISpTableContent<TItem>
{
    public override string PluginName => "table-content";

    [Parameter] public List<TItem>? Items { get; set; }

    [Parameter] public bool Scrollable { get; set; } = true;

    protected override void GenerateClassList(List<string?> list)
    {
        base.GenerateClassList(list);
        if (Scrollable)
        {
            list.Add("s-virtualize");
        }
    }

    public List<TItem>? GetCurrentItems()
    {
        return Items;
    }
}