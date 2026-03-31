using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SProgress : IThemedComponent, ISizedComponent, IContentComponent
{
    protected override string? ComponentClass => "s-progress";

    [Parameter]
    public int Percentage
    {
        get;
        set => SetDirty(ref field, value);
    }

    [Parameter] public SSize Size { get; set; }
    [Parameter] public SColor Color { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public SProgressType Type
    {
        get;
        set => SetDirty(ref field, value);
    } = SProgressType.Bar;

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);
        context.AppendClass(Type);
        context.AppendStyle("--percentage", Percentage.ToString());
    }
}