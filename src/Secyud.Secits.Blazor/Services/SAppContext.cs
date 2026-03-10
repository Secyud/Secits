using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor;

public class SAppContext : IAppContext
{
    public event EventHandler<MouseEventArgs>? AppClickEvent;

    public void OnAppContainerClick(object? sender, MouseEventArgs args)
    {
        AppClickEvent?.Invoke(sender, args);
    }

    public event Action<SDynamicComponentContext>? CreateDynamicComponentEvent;

    public void CreateDynamicComponent(SDynamicComponentContext context)
    {
        CreateDynamicComponentEvent?.Invoke(context);
    }

    public event Action<SDynamicComponentContext>? DeleteDynamicComponentEvent;

    public void DeleteDynamicComponent(SDynamicComponentContext context)
    {
        DeleteDynamicComponentEvent?.Invoke(context);
    }
}