using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

/// <summary>
/// 徽章，提示点
/// </summary>
public partial class SBadge : IContentComponent
{
    protected override string ComponentClass => "s-badge";
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? BadgeContent { get; set; }
}