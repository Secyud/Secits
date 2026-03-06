using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SIcon : ISizedComponent, IThemedComponent
{
    protected override string ComponentClass => "s-icon";
    [Parameter] public SColor Color { get; set; }
    [Parameter] public SSize Size { get; set; }
    [Parameter] public string? Icon { get; set; }
}