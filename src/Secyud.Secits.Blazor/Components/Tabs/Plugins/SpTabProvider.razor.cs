using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

public partial class SpTabProvider<TItem> : ISpTabProvider
{
    public override string PluginName => "tab-provider";
    [Parameter] public List<TItem>? TabItems { get; set; }
    [Parameter] public RenderFragment<TItem>? HeaderTemplate { get; set; }
    [Parameter] public RenderFragment<TItem>? ContentTemplate { get; set; }
    [Parameter] public Func<TItem, string>? Key { get; set; }

    protected void OnTabClicked(string key)
    {
        if (Context?.Component is STabs tabs)
        {
            tabs.SetSelectedTab(key).ConfigureAwait(false);
        }
    }

    public List<STabInfo> GetTabs()
    {
        if (TabItems is not { Count: > 0 } || Key is null)
        {
            return [];
        }

        var tabs = TabItems
            .Select(u =>
            {
                var key = Key(u);
                return new STabInfo
                {
                    Key = key,
                    Header = GenerateTabHeader(u, key),
                    Content = GenerateTabContent(u, key)
                };
            })
            .ToList();

        return tabs;
    }
}