using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor.Plugins;

public abstract partial class SpTableContentBase<TItem>
{
    private STable<TItem>? _table;

    protected STable<TItem>? Table => _table;

    protected List<ISpTableColumn<TItem>> Columns { get; set; } = null!;

    protected override void OnComponentSet()
    {
        base.OnComponentSet();
        SetComponentRef(ref _table);
        SetDirty();
    }

    [Parameter] public string? Class { get; set; }
    [Parameter] public string? Style { get; set; }

    protected void SetDirty()
    {
        _table?.SetDirty();
    }

    protected void SetDirty<T>(ref T? field, T? value) where T : IEquatable<T>
    {
        _table?.SetDirty(ref field, value);
    }

    protected string? GetClass() => Class;
    protected string? GetStyle() => Style;
}