using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor.PageRoutes;

public partial class PageRouteTabsLayout : IDisposable
{
    protected STabContainer? TabContainer { get; set; }

    [Inject]
    protected PageRouteManager PageRouteManager { get; set; } = null!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    protected IIconProvider IconProvider { get; set; } = null!;

    [CascadingParameter]
    public RouteData? RouteData { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            OnLocationChanged(null, new LocationChangedEventArgs("", true));
        }
    }

    protected virtual void OnLocationChanged(object? sender, LocationChangedEventArgs args)
    {
        if (RouteData is not null)
        {
            PageRouteManager.ActivatePageRouteItem(RouteData, NavigationManager.Uri);
            OnActivatedRouteItem().ConfigureAwait(false);
        }
    }

    protected virtual async Task OnActivatedRouteItem()
    {
        if (TabContainer is not null)
        {
            await TabContainer.SelectTabAsync(this, PageRouteManager.CurrentItem?.Id);
            await InvokeAsync(StateHasChanged);
        }
    }

    protected virtual Func<string>? CreateDisplayNameGetter(PageRouteItem item)
    {
        return null;
    }

    protected virtual void CloseTabAsync(PageRouteItem item)
    {
        PageRouteManager.RemovePageRouteItem(item);
        NavigationManager.NavigateTo(PageRouteManager.CurrentItem?.Uri.ToString() ?? "/");
    }

    protected virtual void OnTabOptioned(Tab<PageRouteItem> tab)
    {
        tab.Key = tab.Item.Id;
        tab.PreventDefaultClick = true;
    }


    protected virtual RenderFragment GenerateContent(PageRouteItem context) => context.GenerateBody;

    protected virtual string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-tab-container", "s-page-router", Class);
    }

    protected string? GetStyle()
    {
        return Style;
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}