using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// the common slot.
/// 
/// </summary>
public interface IContentRenderer : IIsPlugin
{
    RendererPosition GetLayoutPosition();
    RenderFragment? RenderTemplate();
}