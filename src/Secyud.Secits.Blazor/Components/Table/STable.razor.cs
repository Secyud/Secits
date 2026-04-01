using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Plugins;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TItem))]
public partial class STable<TItem> : IThemedComponent, IPluggableComponent, ISelectionComponent<TItem>
{
    public STable()
    {
        ColumnContext = new SpTableColumnContext<TItem>
        {
            Table = this
        };
        PluginContext = new SPluginContext(this)
        {
            StateHasChanged = StateHasChanged,
            InvokeAsync = InvokeAsync
        };
        Selection = new SSelection<TItem>(this);
    }

    protected override string ComponentClass => "s-table";
    [Parameter] public SColor Color { get; set; }
    [Parameter] public RenderFragment? Columns { get; set; }
    protected SpTableColumnContext<TItem> ColumnContext { get; }

    public List<DataField> GetDataFields()
    {
        return ColumnContext.Columns
            .Select(u => u.DataField)
            .ToList();
    }

    public List<ISpTableColumn<TItem>> GetColumns()
    {
        return ColumnContext.Columns.ToList();
    }

    [Parameter] public RenderFragment? Plugins { get; set; }
    protected SPluginContext PluginContext { get; }
    protected SPluginContainer<ISpTableContent<TItem>> Content { get; } = new();
    protected SPluginsContainer<ISpTableHeader<TItem>> Header { get; } = new();
    protected SPluginsContainer<ISpTableFooter<TItem>> Footer { get; } = new();
    protected SPluginsContainer<ISpTableElement> Element { get; } = new();
    protected SPluginsContainer<ISpTableStyle> Styles { get; } = new();

    public void ApplyPlugin(ISPlugin plugin)
    {
        Content.TryApply(plugin);
        Header.TryApply(plugin);
        Footer.TryApply(plugin);
        Element.TryApply(plugin);
        Styles.TryApply(plugin);
        InvokeAsync(StateHasChanged);
    }

    public void ForgoPlugin(ISPlugin plugin)
    {
        Content.TryForgo(plugin);
        Header.TryForgo(plugin);
        Footer.TryForgo(plugin);
        Element.TryForgo(plugin);
        Styles.TryForgo(plugin);
        InvokeAsync(StateHasChanged);
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        Selection.SyncParameters(SelectedItem, SelectedItems);
    }

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);
        Styles.Invoke(u => u.BuildClassStyle(context));
    }

    public Task RefreshAsync()
    {
        return InvokeAsync(StateHasChanged);
    }

    #region Selection

    public SSelection<TItem> Selection { get; }
    [Parameter] public Func<TItem, object>? Key { get; set; }
    [Parameter] public TItem? SelectedItem { get; set; }
    [Parameter] public EventCallback<TItem?> SelectedItemChanged { get; set; }

    public async Task SetSelectedItem(TItem? item)
    {
        Selection.SetSelectedItem(item);
        await SelectedItemChanged.InvokeAsync(item);
        await InvokeAsync(StateHasChanged);
    }

    [Parameter] public List<TItem>? SelectedItems { get; set; }

    [Parameter] public EventCallback<List<TItem>?> SelectedItemsChanged { get; set; }

    public async Task SetSelectedItems(List<TItem>? items)
    {
        Selection.SetSelectedItems(items);
        await SelectedItemsChanged.InvokeAsync(items);
        await InvokeAsync(StateHasChanged);
    }

    public List<TItem>? GetCurrentItems()
    {
        return Content.Get()?.GetCurrentItems();
    }

    #endregion
}