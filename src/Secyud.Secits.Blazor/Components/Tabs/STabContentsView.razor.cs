using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public partial class STabContentsView : IHasCustomStyle, ITabListener, IDisposable
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
    public TabRenderMode RenderMode { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public bool AllowEmptyContent { get; set; }

    protected string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-tab-contents", Class);
    }

    protected string? GetStyle()
    {
        return Style;
    }


    public Task TabChangedAsync(object? sender)
    {
        return sender == this ? Task.CompletedTask : InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        TabContainer = null;
    }
}