using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SRow : IContentComponent
{
    protected override string ComponentClass => "s-grid-row";
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <see cref="SGridGap"/>
    /// </summary>
    [Parameter]
    public SGridGap Gap { get; set; }
}