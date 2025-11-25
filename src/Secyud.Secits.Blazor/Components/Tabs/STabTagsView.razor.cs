using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public partial class STabTagsView : IHasCustomStyle, ITabListener
{
    private STabContainer? _tabContainer;

    [CascadingParameter]
    public STabContainer? TabContainer
    {
        get => _tabContainer;
        set
        {
            _tabContainer?.TabListeners.Forgo(this);
            _tabContainer = value;
            _tabContainer?.TabListeners.Apply(this);
        }
    }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    protected string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-tab-tags", Class);
    }

    protected string? GetStyle()
    {
        return Style;
    }

    public Task TabChangedAsync(object? sender)
    {
        return sender == this ? Task.CompletedTask : InvokeAsync(StateHasChanged);
    }

    protected async Task SelectTabAsync(ITab tab)
    {
        if (!tab.PreventDefaultClick)
        {
            if (TabContainer is null || TabContainer.CurrentKey == tab.Key) return;
            await TabContainer.SelectTabAsync(this, tab.Key);
        }

        if (tab.Click is not null)
        {
            await tab.Click();
        }
    }
}