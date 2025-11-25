using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Element;

public abstract class SStyledBase : SElementBase, IHasCustomStyle
{
    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    protected virtual string? GetClass()
    {
        return Class;
    }

    protected virtual string? GetStyle()
    {
        return Style;
    }
}