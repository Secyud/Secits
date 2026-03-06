using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SCard : IThemedComponent, IContentComponent
{
    protected override string ComponentClass => "s-card";
    [Parameter] public SColor Color { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
}