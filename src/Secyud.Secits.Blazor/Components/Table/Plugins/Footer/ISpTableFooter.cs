using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

/// <summary>
/// table footer
/// etc. summary
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface ISpTableFooter<TItem> : ISPlugin
{
    RenderFragment GenerateFooter(ISpTableColumn<TItem> item);
}