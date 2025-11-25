using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public class STitle : SStyledBase, IHasContent
{
    [Parameter, Range(1, 6)]
    public int Level { get; set; } = 3;

    [Parameter]

    public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "h" + Level);
        builder.AddAttributeIfNotEmpty(3, "class", GetClass());
        builder.AddAttributeIfNotEmpty(4, "style", GetStyle());
        builder.AddContent(5, ChildContent);
        builder.CloseElement();
    }

    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-title", Class);
    }
}