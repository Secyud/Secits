using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor;

public interface IAppContext
{
    event EventHandler<MouseEventArgs>? AppClickEvent;
    void OnAppContainerClick(object? sender, MouseEventArgs args);

    event Action<SDynamicComponentContext>? CreateDynamicComponentEvent;
    void CreateDynamicComponent(SDynamicComponentContext context);

    event Action<SDynamicComponentContext>? DeleteDynamicComponentEvent;
    void DeleteDynamicComponent(SDynamicComponentContext context);
}