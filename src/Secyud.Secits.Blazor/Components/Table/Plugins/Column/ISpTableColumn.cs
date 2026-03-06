using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

public interface ISpTableColumn<in TItem> : ISPlugin
{
    DataField DataField { get; }
    SpTableColumnInfo ColumnInfo { get; }
    RenderFragment GenerateColumn(TItem item);
    RenderFragment GenerateCaption();
    object? GetField(TItem item);
}