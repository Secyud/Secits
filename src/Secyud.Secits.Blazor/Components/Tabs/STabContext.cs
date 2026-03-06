namespace Secyud.Secits.Blazor;

public class STabContext
{
    public required STabMode Mode { get; set; }
    public required List<STabModel> Tabs { get; set; }
    public required STabEvent Events { get; set; }
}