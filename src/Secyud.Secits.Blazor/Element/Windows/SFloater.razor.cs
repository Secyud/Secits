using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor.Element;

public partial class SFloater : IContentComponent, IContainerComponent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-floater", Class);
    }

    protected override string? GetStyle()
    {
        return Style;
    }
}