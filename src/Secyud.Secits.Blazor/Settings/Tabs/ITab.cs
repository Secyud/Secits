using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings.Tabs;

public interface ITab
{
    string Key { get; }
    bool IsRendered { get; set; }
    bool PreventDefaultClick { get; }
    Func<Task>? Click { get; }
    RenderFragment? RenderTab();
    RenderFragment? RenderTabContent();
}