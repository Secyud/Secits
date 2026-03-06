using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Navigation;

namespace Secyud.Secits.Blazor.Plugins;

public partial class SpRouterTabProvider : ISpTabProvider
{
    public override string PluginName => "router-tab-provider";
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Parameter] public List<RouterItem>? TabItems { get; set; }
    [Parameter] public RenderFragment<RouterItem>? HeaderTemplate { get; set; }
    [Parameter] public RenderFragment<RouterItem>? ContentTemplate { get; set; }

    /// <summary>
    /// 路由tab不同于普通的切换，需要改变路由
    /// </summary>
    /// <param name="item"></param>
    protected void OnTabClicked(RouterItem item)
    {
        NavigationManager.NavigateTo(item.Uri.ToString());
    }

    public List<STabInfo> GetTabs()
    {
        if (TabItems is not { Count: > 0 })
        {
            return [];
        }

        var tabs = TabItems
            .Select(u => new STabInfo
            {
                Key = u.Id.ToString(),
                Header = GenerateTabHeader(u),
                Content = GenerateTabContent(u)
            })
            .ToList();

        return tabs;
    }
}