using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

/// <summary>
/// the component has a child content.
/// </summary>
public interface IContentComponent
{
    RenderFragment? ChildContent { get; set; }
}

/// <summary>
/// the component has a child content.
/// </summary>
public interface IContentComponent<TContext>
{
    RenderFragment<TContext>? ChildContent { get; set; }
}