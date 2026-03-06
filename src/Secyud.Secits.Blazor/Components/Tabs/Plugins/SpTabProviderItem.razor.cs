using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

public partial class SpTabProviderItem : IContentComponent
{
    public override string PluginName => "tab-provider-item";
    [Parameter] public string Key { get; set; } = Guid.NewGuid().ToString("N");
    [Parameter] public RenderFragment? Header { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [CascadingParameter]
    protected SpTabProviderItems? Master
    {
        get;
        set
        {
            if (field == value) return;
            field?.Items.TryForgo(this);
            field = value;
            field?.Items.TryApply(this);
        }
    }

    protected void OnTabClicked()
    {
        if (Context?.Component is STabs tabs)
        {
            tabs.SetSelectedTab(Key).ConfigureAwait(false);
        }
    }

    protected override void Dispose(bool isDisposing)
    {
        base.Dispose(isDisposing);
        if (isDisposing)
        {
            Master = null;
        }
    }
}