using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SButton : IThemedComponent, ISizedComponent, IContentComponent, IActivableComponent,
    IClickableComponent
{
    protected override string ComponentClass => "s-button";

    /// <summary>
    /// <see cref="SColor"/>
    /// </summary>
    [Parameter]
    public SValue Color { get; set; }

    /// <summary>
    /// <see cref="SSize"/>
    /// </summary>
    [Parameter]
    public SValue Size { get; set; }

    [Parameter] public bool Disabled { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public EventCallback Click { get; set; }
    [Parameter] public string? Icon { get; set; }
}