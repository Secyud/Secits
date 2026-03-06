using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor.Plugins;

public interface ISpInputElement : ISPlugin
{
    void GenerateInputElement(RenderTreeBuilder builder);
}