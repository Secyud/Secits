using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

/// <summary>
/// 按钮
/// </summary>
public partial class SButton : IThemedComponent, ISizedComponent, IContentComponent, IActivableComponent,
    IClickableComponent
{
    protected override string ComponentClass => "s-button";

    /// <summary>
    /// <see cref="SColor"/>
    /// </summary>
    [Parameter]
    public SColor Color { get; set; }

    /// <summary>
    /// <see cref="SSize"/>
    /// </summary>
    [Parameter]
    public SSize Size { get; set; }

    [Parameter] public bool Disabled { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public EventCallback Click { get; set; }
    [Parameter] public string? Icon { get; set; }
}