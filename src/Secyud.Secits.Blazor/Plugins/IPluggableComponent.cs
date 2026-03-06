using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

public interface IPluggableComponent
{
    RenderFragment? Plugins { get; }
    void ApplyPlugin(ISPlugin plugin);
    void ForgoPlugin(ISPlugin plugin);
}