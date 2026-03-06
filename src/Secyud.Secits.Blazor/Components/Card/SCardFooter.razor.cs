using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SCardFooter : IContentComponent
{
    protected override string ComponentClass => "s-card-footer";
    [Parameter] public RenderFragment? ChildContent { get; set; }
}