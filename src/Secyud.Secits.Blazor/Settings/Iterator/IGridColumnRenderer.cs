using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// render settings for a table column.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IGridColumnRenderer<in TValue> : IIsPlugin, IListColumnRenderer<TValue>, IHasCustomStyle
{
    RenderFragment GenerateHeader();
    RenderFragment GenerateFooter();
    int RealWidth { get; set; }
    int Width { get;  }
    int MaxWidth { get; }
    int MinWidth { get; }
    ColumnPosition Position { get; }
}