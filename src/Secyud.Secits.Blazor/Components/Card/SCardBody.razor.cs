using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SCardBody : IContentComponent
{
    protected override string ComponentClass => "s-card-body";
    [Parameter] public RenderFragment? ChildContent { get; set; }
}