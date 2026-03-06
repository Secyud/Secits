using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SAvatar : ISizedComponent
{
    protected override string ComponentClass => "s-avatar";

    /// <summary>
    /// <see cref="SSize"/>
    /// </summary>
    [Parameter]
    public SSize Size { get; set; }

    /// <summary>
    /// <see cref="SAvatarShape"/>
    /// </summary>
    [Parameter]
    public SAvatarShape Shape { get; set; } = SAvatarShape.Circle;

    [Parameter] public string? Src { get; set; }
    [Parameter] public string? Alt { get; set; }
    [Parameter] public string? Text { get; set; }

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);
        context.AppendClass(Shape);
    }
}