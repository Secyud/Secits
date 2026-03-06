using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SCardHeader : IContentComponent
{
    protected override string ComponentClass => "s-card-header";
    [Parameter] public RenderFragment? ChildContent { get; set; }
}