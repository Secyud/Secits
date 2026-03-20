using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Plugins;

namespace Secyud.Secits.Blazor;

public partial class STabs : IPluggableComponent, IContentComponent
{
    public STabs()
    {
        PluginContext = new SPluginContext(this)
        {
            StateHasChanged = StateHasChanged,
            InvokeAsync = InvokeAsync
        };
    }

    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? Plugins { get; set; }
    [Parameter] public STabMode Mode { get; set; }
    protected SPluginContext PluginContext { get; }
    protected SPluginContainer<ISpTabProvider> TabProvider { get; } = new();
    protected Dictionary<string, STabStatus> TabStatus { get; } = new();
    protected STabEvent Event { get; } = new();

    public STabContext GetTabContext()
    {
        var infos = TabProvider.Get()?.GetTabs() ?? [];
        var tabs = infos.Select(u => new STabModel
        {
            Info = u,
            Status = TabStatus.GetValueOrDefault(u.Key, new STabStatus()
            {
                IsRender = Mode == STabMode.LoadAll
            })
        }).ToList();
        TabStatus.Clear();
        foreach (var tab in tabs)
        {
            TabStatus.Add(tab.Info.Key, tab.Status);
        }

        return new STabContext
        {
            Mode = Mode,
            Tabs = tabs,
            Events = Event
        };
    }

    public async Task SetSelectedTab(string key)
    {
        var selectedStatus = TabStatus.GetValueOrDefault(key);

        if (selectedStatus is { IsActive: true }) return;

        foreach (var status in TabStatus.Values)
        {
            status.IsActive = false;
        }

        selectedStatus?.IsActive = true;
        await InvokeAsync(StateHasChanged);
    }

    public void ApplyPlugin(ISPlugin plugin)
    {
        TabProvider.TryApply(plugin);
    }

    public void ForgoPlugin(ISPlugin plugin)
    {
        TabProvider.TryForgo(plugin);
    }
}