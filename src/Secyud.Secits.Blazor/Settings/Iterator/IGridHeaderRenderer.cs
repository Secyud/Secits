using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// setting for all column header.
/// etc. resizer, filter.
/// </summary>
public interface IGridHeaderRenderer : IIsPlugin
{
    RenderFragment GenerateHeader(int index);
}