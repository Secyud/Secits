using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

/// <summary>
/// table header
/// etx, filter
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface ISpTableHeader<TItem> : ISPlugin
{
    RenderFragment GenerateHeader(ISpTableColumn<TItem> item);
}