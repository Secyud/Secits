namespace Secyud.Secits.Blazor.Settings.Tabs;

public interface ITabsProvider
{
    IEnumerable<ITab> GetTabs();
}