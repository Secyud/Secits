using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor.Element;

public class SBadge : SStyledBase
{
    [Parameter]
    public string? Text { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttributeIfNotEmpty(3, "class", GetClass());
        builder.AddAttributeIfNotEmpty(4, "style", GetStyle());
        builder.AddContent(5, Text);
        builder.CloseElement();
    }

    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-badge", Class);
    }
}