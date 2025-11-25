using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor.Element;

public class SLabel : SLayoutPluginBase<SContentBase>
{
    [Parameter]
    public string? For { get; set; }

    [Parameter]
    public string? Name { get; set; }

    [Parameter]
    public string? Text { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "label");
        builder.AddAttributeIfNotEmpty(1, "for", For);
        builder.AddAttributeIfNotEmpty(2, "name", Name);
        builder.AddAttributeIfNotEmpty(3, "class", GetClass());
        builder.AddAttributeIfNotEmpty(4, "style", GetStyle());
        builder.AddContent(5, Text);
        builder.CloseElement();
    }

    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-label", Class);
    }
}