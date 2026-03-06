using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SFlex : IContentComponent
{
    protected override string ComponentClass => "s-flex";
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <see cref="SFlexDirection"/>
    /// </summary>
    [Parameter]
    public SFlexDirection FlexDirection { get; set; }

    /// <summary>
    /// <see cref="SFlexGap"/>
    /// </summary>
    [Parameter]
    public SFlexGap Gap { get; set; }

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);

        context.AppendClassOrStyle(FlexDirection, styleName: "flex-direction");
        context.AppendClassOrStyle(Gap, styleName: "gap");
    }
}