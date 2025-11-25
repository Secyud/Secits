using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor.Element;

public class SDivider : SStyledBase
{
    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-divider", Class);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "hr");
        builder.AddAttributeIfNotEmpty(1, "class", GetClass());
        builder.AddAttributeIfNotEmpty(2, "style", GetStyle());
        builder.CloseElement();
    }
}