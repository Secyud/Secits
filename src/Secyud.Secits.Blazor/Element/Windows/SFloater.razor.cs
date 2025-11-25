using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public partial class SFloater : IHasContent, IHasCustomStyle
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