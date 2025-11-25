using System.Text;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class FormLayoutItem : SLayoutPluginBase<SFormLayout>, IHasContent
{
    [Parameter]
    public int ColSpanXs { get; set; } = 12;

    [Parameter]
    public int ColSpanSm { get; set; } = 6;

    [Parameter]
    public int ColSpanMd { get; set; } = 4;

    [Parameter]
    public int ColSpanLg { get; set; } = 3;

    [Parameter]
    public int ColSpanXl { get; set; } = 2;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public RenderFragment? TitleTemplate { get; set; }

    [Parameter]
    public bool EnableValidation { get; set; } = true;

    [Parameter]
    public bool ShowValidationMessage { get; set; }

    protected override string GetClass()
    {
        var sb = new StringBuilder();
        sb.Append("form-item");
        var basicClass = base.GetClass();
        if (!string.IsNullOrEmpty(basicClass))
            sb.Append($" {basicClass}");
        if (ColSpanXs != 12)
            sb.Append($" col-xs-{ColSpanXs}");
        if (ColSpanSm != 6)
            sb.Append($" col-sm-{ColSpanSm}");
        if (ColSpanMd != 4)
            sb.Append($" col-md-{ColSpanMd}");
        if (ColSpanLg != 3)
            sb.Append($" col-lg-{ColSpanLg}");
        if (ColSpanXl != 2)
            sb.Append($" col-xl-{ColSpanXl}");
        return sb.ToString();
    }
}