using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Element;

public partial class SProgressBar
{
    [Parameter]
    public int Percentage { get; set; }

    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-progress-bar", Class);
    }

    protected override string GetStyle()
    {
        return $"--percentage:{Percentage};{Style}";
    }
}