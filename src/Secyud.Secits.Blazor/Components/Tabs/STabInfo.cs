using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class STabInfo
{
    public required string Key { get; set; }
    public required RenderFragment<STabStatus> Header { get; set; }
    public required RenderFragment<STabStatus> Content { get; set; }
}