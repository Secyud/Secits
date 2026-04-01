using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SDropdown : IContentComponent
{
    protected override string ComponentClass => "s-dropdown";
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? DropdownContent { get; set; }
}