using Microsoft.AspNetCore.Components;

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
    }

    [Parameter] public string? Class { get; set; }
    [Parameter] public string? Style { get; set; }

    private readonly List<string?> _classList = [];

    protected virtual void GenerateClassList(List<string?> list)
    {
        list.Add(Class);
    }

    protected string? GetClass()
    {
        _classList.Clear();
        GenerateClassList(_classList);
        return JoinString(_classList);
    }

    protected string? GetStyle() => Style;
}