using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// the component has a child content.
/// </summary>
public interface IHasContent
{
    RenderFragment? ChildContent { get; set; }
}

public interface IHasContent<TContext>
{
    RenderFragment<TContext>? ChildContent { get; set; }
}