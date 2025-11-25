using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// represent the row setting.
/// click action is useful for selection.
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface IRowClickEvent<in TItem> : IIsPlugin
{
    Task OnRowClick(MouseEventArgs args, TItem item);
}