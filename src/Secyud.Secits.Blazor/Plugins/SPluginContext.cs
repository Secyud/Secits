namespace Secyud.Secits.Blazor.Plugins;

public class SPluginContext(IPluggableComponent component)
{
    public IPluggableComponent Component { get; } = component;
    public required Action StateHasChanged { get; init; }
    public required Func<Action, Task> InvokeAsync { get; init; }
}