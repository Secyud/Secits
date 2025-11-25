using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract partial class SActivableBase : ICanActive, ICanClick
{
    #region Parameters

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string? ColTemplate { get; set; }

    [Parameter]
    public string? RowTemplate { get; set; }

    #endregion

    protected override void BuildClassStyle(ClassStyleContext context)
    {
        base.BuildClassStyle(context);
        context.AppendStyle("grid-template-columns", ColTemplate);
        context.AppendStyle("grid-template-rows", RowTemplate);
    }


    [Parameter]
    public EventCallback Click { get; set; }

    protected virtual void OnClick()
    {
        Click.InvokeAsync().ConfigureAwait(false);
    }
}