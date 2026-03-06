using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor;

public partial class STitle : IContentComponent
{
    protected override string ComponentClass => "s-title";
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter, Range(1, 6)] public int Level { get; set; } = 3;

    protected void BuildTitle(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "h" + Level);
        builder.AddElementReferenceCapture(1, u => ElementRef = u);
        builder.AddAttributeIfNotEmpty(2, "class", GetClass());
        builder.AddAttributeIfNotEmpty(3, "style", GetStyle());
        builder.AddContent(4, ChildContent);
        builder.CloseElement();
    }
}