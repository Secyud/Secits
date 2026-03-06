using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

public interface ISpTableContent<TItem> : ISPlugin
{
    RenderFragment GenerateContent(List<ISpTableColumn<TItem>> columns);

    List<TItem>? GetCurrentItems();
}