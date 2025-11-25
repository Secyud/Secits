using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public abstract partial class SIconBase : ICanClick
{
    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public EventCallback Click { get; set; }

    protected abstract string? Icon { get; }

    protected virtual void OnClick(MouseEventArgs args)
    {
        Click.InvokeAsync(args);
    }

    protected virtual string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-icon", Class);
    }

    protected string? GetStyle()
    {
        return Style;
    }
}