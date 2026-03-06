namespace Secyud.Secits.Blazor.Plugins;

public class SPluginContext(IPluggableComponent component)
{
    public IPluggableComponent Component { get; } = component;
}