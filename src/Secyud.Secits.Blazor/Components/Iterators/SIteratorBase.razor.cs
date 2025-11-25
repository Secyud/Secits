using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract partial class SIteratorBase<TValue>
{
    public DataRequest DataRequest { get; } = new();

    protected virtual string? GetRowClass(TValue value)
    {
        var @class = ComponentName + "-row ";
        var styles = RowClassProviders.Select(u => u.GetRowClass(value))
            .Where(u => !string.IsNullOrEmpty(u))
            .Cast<string>()
            .ToList();
        return @class + string.Join(" ", styles);
    }

    protected string GetRowStyle(TValue value)
    {
        var styles = RowStyleProviders.Select(u => u.GetRowStyle(value))
            .Where(u => !string.IsNullOrEmpty(u))
            .Cast<string>()
            .ToList();
        return string.Join(null, styles);
    }

    protected void OnRowClick(MouseEventArgs args, TValue value)
    {
        RowClickEvents.InvokeAsync(u => u.OnRowClick(args, value))
            .ConfigureAwait(false);
    }

    #region Settings

    public SSettings<IRowClickEvent<TValue>> RowClickEvents { get; } = new();
    public SSettings<IRowStyleProvider<TValue>> RowStyleProviders { get; } = new();
    public SSettings<IRowClassProvider<TValue>> RowClassProviders { get; } = new();
    public SSetting<IDataSourceProvider<TValue>> DataSource { get; } = new();
    public SSetting<IIteratorRenderer<TValue>> ItemsRenderer { get; } = new();


    public Task RefreshAsync(bool resetState)
    {
        return ItemsRenderer.InvokeAsync(u => u.RefreshAsync(resetState));
    }

    #endregion
}