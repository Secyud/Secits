using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public partial class TabProvider<TItem> : ITabsProvider
    where TItem : class
{
    [Parameter]
    public IReadOnlyList<TItem> Items { get; set; } = [];

    [Parameter]
    public RenderFragment<TItem>? TagTemplate { get; set; }

    [Parameter]
    public RenderFragment<TItem>? ContentTemplate { get; set; }

    [Parameter]
    public Action<Tab<TItem>>? Options { get; set; }


    private List<Tab<TItem>> _tabs = [];

    protected override void ApplySetting()
    {
        Master.TabProviders.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.TabProviders.Forgo(this);
    }

    public IEnumerable<ITab> GetTabs()
    {
        var tabs = _tabs;
        _tabs = [];
        foreach (var item in Items)
        {
            Tab<TItem> tab;
            var index = tabs.FindIndex(u => u.Item == item);
            if (index < 0)
            {
                tab = CreateTabFromItem(item);
            }
            else
            {
                tab = tabs[index];
                tabs.RemoveAt(index);
            }

            Options?.Invoke(tab);
            _tabs.Add(tab);
        }

        return _tabs;
    }

    private Tab<TItem> CreateTabFromItem(TItem item)
    {
        var tab = new Tab<TItem>(item);
        if (ContentTemplate is not null)
        {
            tab.Content = ContentTemplate(item);
        }

        if (TagTemplate is not null)
        {
            tab.Tag = TagTemplate(item);
        }


        return tab;
    }
}