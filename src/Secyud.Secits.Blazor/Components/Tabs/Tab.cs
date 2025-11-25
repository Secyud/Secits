using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public class Tab<TItem>(TItem item) : ITab
{
    public TItem Item { get;} = item;
    public RenderFragment? Tag { get; set; }

    public RenderFragment? Content { get; set; }

    public string Key { get; set; } = Guid.NewGuid().ToString("N");

    public Func<Task>? Click { get; set; }

    public bool PreventDefaultClick { get; set; }

    public bool IsRendered { get; set; }

    public RenderFragment? RenderTab() => Tag;

    public RenderFragment? RenderTabContent() => Content;
}