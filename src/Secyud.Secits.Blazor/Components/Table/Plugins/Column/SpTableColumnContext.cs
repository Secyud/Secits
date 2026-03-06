namespace Secyud.Secits.Blazor.Plugins;

public class SpTableColumnContext<TItem>
{
    public required STable<TItem> Table { get; set; }
    public SPluginsContainer<ISpTableColumn<TItem>> Columns { get; } = new();

    public void TryApplyColumn(ISpTableColumn<TItem> column)
    {
        Columns.TryApply(column);
    }

    public void TryForgoColumn(ISpTableColumn<TItem> column)
    {
        Columns.TryForgo(column);
    }
}