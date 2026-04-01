using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class STabHeader
{
    protected override string? ComponentClass => "s-tab-header";

    [CascadingParameter] protected STabContext? Context { get; set; }

    [Parameter]
    public STabDirection Direction
    {
        get;
        set => SetDirty(ref field, value);
    }

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);
        context.AppendClass(Direction);
    }
}