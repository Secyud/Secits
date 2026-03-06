using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SGridRow : IContentComponent
{
    protected override string ComponentClass => "s-grid-row";
    [Parameter] public RenderFragment? ChildContent { get; set; }
}