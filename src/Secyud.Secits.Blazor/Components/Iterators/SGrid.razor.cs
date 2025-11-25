using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

/// <summary>
/// table组件,可以展示多行数据.
/// </summary>
/// <typeparam name="TValue"></typeparam>
[CascadingTypeParameter(nameof(TValue))]
public partial class SGrid<TValue>
{
    protected override string ComponentName => "grid";

    [Parameter]
    public bool DisableHeader { get; set; }

    [Parameter]
    public bool DisableFooter { get; set; }

    [Parameter]
    public bool AutoWidth { get; set; }

    #region Settings

    public SSettings<IGridColumnRenderer<TValue>> TableColumns { get; } = new();
    public SSettings<IGridHeaderRenderer> TableHeaders { get; } = new();
    public SSettings<IGridFooterRenderer> TableFooters { get; } = new();

    protected override string? GetRowClass(TValue value)
    {
        return base.GetRowClass(value) + " body";
    }

    #endregion
}