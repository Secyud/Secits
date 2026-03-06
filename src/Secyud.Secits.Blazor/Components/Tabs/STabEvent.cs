namespace Secyud.Secits.Blazor;

public class STabEvent
{
    public event Action<string>? TabSelected;

    public void OnTabSelected(string tabName)
    {
        TabSelected?.Invoke(tabName);
    }
}