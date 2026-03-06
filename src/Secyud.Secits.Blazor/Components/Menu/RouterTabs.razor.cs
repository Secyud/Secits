using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Secyud.Secits.Blazor.Navigation;

namespace Secyud.Secits.Blazor;

public partial class RouterTabs : IDisposable
{
    protected override string? ComponentClass => "s-router-tab";
    [Inject] protected IRouterItemGenerator RouterItemGenerator { get; set; } = null!;

    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

    [CascadingParameter] public RouteData? RouteData { get; set; }

    protected STabs? Tabs { get; set; }
    protected List<RouterItem> Items { get; } = [];
    protected RouterItem? CurrentItem { get; set; }

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
            // 组件实例化时，当前路由一定有值，防止初始化时没有标签。
            await CreateTabAsync();
        }
    }

    protected virtual void OnLocationChanged(object? sender, LocationChangedEventArgs args)
    {
        CreateTabAsync().ConfigureAwait(false);
    }

    protected virtual async Task CreateTabAsync()
    {
        if (RouteData is null || !Uri.TryCreate(NavigationManager.Uri, UriKind.Absolute, out var uri)) return;
        CurrentItem = Items.FirstOrDefault(u => u.Uri == uri);
        if (CurrentItem is null)
        {
            var item = RouterItemGenerator.Create(RouteData, uri);
            Items.Add(item);
            CurrentItem = item;
        }

        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task DeleteTabAsync(RouterItem item)
    {
        var index = Items.IndexOf(item);
        if (index >= 0)
        {
            Items.RemoveAt(index);
            index -= 1;
        }

        if (item == CurrentItem)
        {
            if (Items.Count > 0)
            {
                index = Math.Max(0, index);
                CurrentItem = Items[index];
                NavigationManager.NavigateTo(CurrentItem.Uri.ToString());
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }

            return;
        }

        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}