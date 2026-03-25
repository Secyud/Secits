using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SMenu<TItem> : IContentComponent<TItem>
{
    protected override string ComponentClass => "s-menu";
    [Parameter] public RenderFragment<TItem>? ChildContent { get; set; }
    [Parameter] public IList<TItem>? Items { get; set; }
    [Parameter] public Func<TItem, bool>? Collapsed { get; set; }
    [Parameter] public Func<TItem, IList<TItem>>? Children { get; set; }
}