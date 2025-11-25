using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public partial class FixTab : ITab, ITabsProvider
{
    private ITab[]? _tabs;

    [Parameter]
    public RenderFragment? Tag { get; set; }

    [Parameter]
    public RenderFragment? Content { get; set; }

    [Parameter]
    public string Key { get; set; } = Guid.NewGuid().ToString("N");

    [Parameter]
    public Func<Task>? Click { get; set; }

    [Parameter]
    public bool PreventDefaultClick { get; set; }

    public bool IsRendered { get; set; }

    public RenderFragment? RenderTab() => Tag;

    public RenderFragment? RenderTabContent() => Content;

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
        _tabs ??= [this];
        return _tabs;
    }
}