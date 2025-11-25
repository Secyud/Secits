using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public partial class STabContainer:IPluggable
{
    protected SPluggableContainer PluggableContainer { get; }

    public STabContainer()
    {
        PluggableContainer = new SPluggableContainer(this);
    }
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    public string? CurrentKey { get; set; }

    public SSettings<ITabsProvider> TabProviders { get; } = [];

    public SSettings<ITabListener> TabListeners { get; } = new();

    public async Task SelectTabAsync(object? sender, string? tabKey)
    {
        CurrentKey = tabKey;
        await TabListeners.InvokeAsync(u => u.TabChangedAsync(sender));
    }

    void IPluggable.StateHasChanged()
    {
        StateHasChanged();
    }

    Task IPluggable.InvokeAsync(Action action)
    {
        return InvokeAsync(action);
    }

    Task IPluggable.InvokeAsync(Func<Task> action)
    {
        return InvokeAsync(action);
    }
}