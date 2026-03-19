using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SAppContainer : IContentComponent, ISizedComponent, IThemedComponent, IDisposable
{
    protected override string ComponentClass => "s-app-container";
    [Inject] protected IAppContext AppContext { get; set; } = null!;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public SSize Size { get; set; }
    [Parameter] public SColor Color { get; set; }

    protected List<SDynamicComponentContext> DynamicComponents { get; } = [];

    protected override void OnInitialized()
    {
        AppContext.CreateDynamicComponentEvent += CreateDynamicComponent;
        AppContext.DeleteDynamicComponentEvent += DeleteDynamicComponent;
    }

    protected void OnClick(MouseEventArgs args)
    {
        AppContext.OnAppContainerClick(this, args);
    }

    protected void CreateDynamicComponent(SDynamicComponentContext context)
    {
        DynamicComponents.Add(context);
        StateHasChanged();
    }

    protected void DeleteDynamicComponent(SDynamicComponentContext context)
    {
        DynamicComponents.Remove(context);
        StateHasChanged();
    }

    public void Dispose()
    {
        AppContext.CreateDynamicComponentEvent -= CreateDynamicComponent;
        AppContext.DeleteDynamicComponentEvent -= DeleteDynamicComponent;
    }
}