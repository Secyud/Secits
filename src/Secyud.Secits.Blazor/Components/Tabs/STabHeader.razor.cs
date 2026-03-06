using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class STabHeader
{
    protected override string? ComponentClass => "s-tab-header";

    [CascadingParameter] protected STabContext? Context { get; set; }
}