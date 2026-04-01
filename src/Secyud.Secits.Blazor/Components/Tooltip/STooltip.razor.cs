using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class STooltip : IContentComponent
{
    private bool _visible;
    protected override string ComponentClass => "s-tooltip";
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? TooltipContent { get; set; }
}