using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

public partial class SpTabProviderItems : ISpTabProvider, IContentComponent
{
    public string PluginName => "tab-provider-items";

    [Parameter] public RenderFragment? ChildContent { get; set; }
    public SPluginsContainer<SpTabProviderItem> Items { get; } = [];

    [CascadingParameter]
    protected SPluginContext? Context
    {
        get;
        set
        {
            if (field == value) return;
            field?.Component.ForgoPlugin(this);
            field = value;
            field?.Component.ApplyPlugin(this);
        }
    }

    public List<STabInfo> GetTabs()
    {
        if (Items is not { Count: > 0 })
        {
            return [];
        }

        var tabs = Items
            .Select(u => new STabInfo
            {
                Key = u.Key,
                Header = u.GenerateTabHeader,
                Content = u.GenerateTabContent
            })
            .ToList();

        return tabs;
    }
}