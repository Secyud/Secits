using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

/// <summary>
/// 分割线
/// </summary>
public partial class SDivider : IThemedComponent, ISizedComponent
{
    protected override string ComponentClass => "s-divider";
    [Parameter] public SColor Color { get; set; } = SColor.Naive;
    [Parameter] public SSize Size { get; set; }
}