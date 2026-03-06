using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SGridCol : IContentComponent
{
    protected override string ComponentClass => "s-grid-col";
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <see cref="SGridSpan"/>
    /// </summary>
    [Parameter]
    public SGridSpan Span { get; set; }

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);
        context.AppendClass(Span);
    }
}