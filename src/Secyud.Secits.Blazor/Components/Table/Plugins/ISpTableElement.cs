using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

public interface ISpTableElement : ISPlugin
{
    STablePosition Position { get; }
    RenderFragment GenerateElement();
}