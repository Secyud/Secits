using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class STooltip : IContentComponent
{
    protected override string ComponentClass => "s-tooltip";
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? TooltipContent { get; set; }
}