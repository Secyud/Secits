using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class STabContent
{
    protected override string ComponentClass => "s-tab-content";

    [CascadingParameter] protected STabContext? Context { get; set; }
}