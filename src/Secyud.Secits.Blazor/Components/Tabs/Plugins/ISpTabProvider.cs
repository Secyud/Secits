namespace Secyud.Secits.Blazor.Plugins;

public interface ISpTabProvider : ISPlugin
{
    List<STabInfo> GetTabs();
}