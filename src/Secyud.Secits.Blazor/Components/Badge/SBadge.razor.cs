using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SBadge : IContentComponent
{
    protected override string ComponentClass => "s-badge";
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? BadgeContent { get; set; }
}