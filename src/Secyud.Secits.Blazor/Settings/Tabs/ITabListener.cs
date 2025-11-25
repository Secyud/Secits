namespace Secyud.Secits.Blazor.Settings.Tabs;

public interface ITabListener
{
    Task TabChangedAsync(object? sender);
}