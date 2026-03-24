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
    public SFormGap Gap { get; set; } = SFormGap.Medium;

    protected SValidationGroupContext Context { get; } = new();

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);

        context.AppendClassOrStyle(Gap, styleName: "grid-gap");
    }

    public bool IsValid => Context.IsValid();
}