using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SForm : IContentComponent
{
    protected override string? ComponentClass => "s-form";
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <see cref="SFormGap"/>
    /// </summary>
    [Parameter]
    public SValue Gap { get; set; }

    public SValidationGroupContext Context { get; } = new();

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);

        context.AppendClassOrStyle(Gap, styleName: "grid-gap");
    }
}