using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Plugins;

public partial class SpTableColumn<TItem, TField> : ISpTableColumn<TItem>
{
    public override string PluginName => "table-column";

    private DataFieldExpression<TItem, TField>? _fieldExpression;

    [Parameter] public string? Format { get; set; }

    [Parameter]
    public Expression<Func<TItem, TField>>? Field
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            _fieldExpression = value is null
                ? null
                : new DataFieldExpression<TItem, TField>(value);
        }
    }

    [Parameter] public string? Caption { get; set; }

    [Parameter]
    public int? Width
    {
        get;
        set
        {
            field = value;
            ColumnInfo.Width = value;
        }
    }

    protected override string? GetFiledName()
    {
        return _fieldExpression?.GetFieldName();
    }

    public object? GetField(TItem item)
    {
        return _fieldExpression?.GetField(item) ?? default(TField);
    }
}