using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Icons;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class GridColumn<TValue, TField> :
    IGridColumnRenderer<TValue>, IHasField<TValue, TField>
{
    private const int DefaultWidth = 100;

    private ValueFieldOperator<TValue, TField>? _valueField;
    private int _columnWidth = DefaultWidth;
    private RenderFragment? _columnFilter;

    [Inject]
    private IIconProvider IconProvider { get; set; } = null!;

    [Parameter]
    public string? Caption { get; set; }

    [Parameter]
    public int MaxWidth { get; set; } = int.MaxValue;

    [Parameter]
    public int MinWidth { get; set; } = 50;

    [Parameter]
    public ColumnPosition Position { get; set; } = ColumnPosition.Middle;

    [Parameter]
    public int Width { get; set; } = DefaultWidth;

    [Parameter]
    public string? Format { get; set; }

    [Parameter]
    public Expression<Func<TValue, TField>>? Field { get; set; }

    [Parameter]
    public bool EnableFilter { get; set; }

    [Parameter]
    public bool EnableSorter { get; set; }

    [Parameter]
    public RenderFragment<DataFilter>? FilterTemplate { get; set; }

    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    [Parameter]
    public RenderFragment<TValue>? ColumnTemplate { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    public int RealWidth
    {
        get => _columnWidth;
        set =>
            _columnWidth = Math.Clamp(value, MinWidth,
                MaxWidth > MinWidth ? MaxWidth : int.MaxValue);
    }

    #region LifeCycle

    protected override void PreParametersSet(ParameterContainer parameters)
    {
        parameters.UseParameter(Field, nameof(Field), field =>
        {
            var name = GetBodyName(field);
            _valueField = field is not null
                ? new ValueFieldOperator<TValue, TField>(field)
                : null;
            Filter.Field = name;
            Sorter.Field = name;
        });

        parameters.UseParameter(Width, nameof(Width), width => RealWidth = width);
    }

    private static string? GetBodyName(Expression<Func<TValue, TField>>? expr)
    {
        if (expr is null) return null;

        var body = expr.Body;
        if (body is UnaryExpression unary)
            body = unary.Operand;

        var name = body.ToString();
        var index = name.IndexOf('.') + 1;
        return index <= 0 ? null : name[index..];
    }


    protected override void ApplySetting()
    {
        Master.TableColumns.Apply(this);
        Master.DataRequest.Filters.Add(Filter);
        Master.DataRequest.Sorters.Add(Sorter);
    }

    protected override void ForgoSetting()
    {
        Master.TableColumns.Forgo(this);
        Master.DataRequest.Filters.Remove(Filter);
        Master.DataRequest.Sorters.Remove(Sorter);
    }

    #endregion

    protected async Task SetFilterValueAsync(object? filterValue)
    {
        Filter.FilterValue = filterValue;
        await Master.RefreshAsync(true);
    }

    protected string? GetSorterIconClass()
    {
        if (!Sorter.Enabled)
            return IconProvider.GetIcon(IconName.None);

        if (!Sorter.Desc)
            return IconProvider.GetIcon(IconName.DownAngle);

        return IconProvider.GetIcon(IconName.UpAngle);
    }

    protected async Task SetSorterValueAsync()
    {
        if (!Sorter.Enabled)
        {
            Sorter.Enabled = true;
            Sorter.Desc = false;
        }
        else if (!Sorter.Desc)
        {
            Sorter.Desc = true;
        }
        else
        {
            Sorter.Enabled = false;
        }

        await Master.RefreshAsync(true);
    }

    protected TField? GetField(TValue? item)
    {
        return _valueField is null || item is null ? default : _valueField.GetField(item);
    }

    private DataFilter Filter { get; } = new();
    private DataSorter Sorter { get; } = new();
}