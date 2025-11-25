using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class AddContentBase : SLayoutPluginBase<SContentBase>, IHasContent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttributeIfNotEmpty(1, "class", GetClass());
        builder.AddAttributeIfNotEmpty(2, "style", GetStyle());
        builder.AddContent(3, ChildContent);
        builder.CloseElement();
    }
}