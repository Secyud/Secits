using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor;

/// <summary>
/// the inheritor can be clicked
/// </summary>
public interface IClickableComponent
{
    EventCallback Click { get; }

    void OnClick(MouseEventArgs args)
    {
        Click.InvokeAsync(args).ConfigureAwait(false);
    }
}