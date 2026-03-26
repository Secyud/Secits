using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SProgress
{
    protected override string? ComponentClass => "s-progress";

    [Parameter]
    public int Percentage
    {
        get;
        set => SetDirty(ref field, value);
    }

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);
        context.AppendStyle("--percentage", Percentage.ToString());
    }
}