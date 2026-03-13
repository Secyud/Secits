using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Secyud.Secits.Blazor.JsInterop;
using Secyud.Secits.Blazor.JSInterop;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

/// <summary>
/// 浮在所有组件上方的组件，可以控制显示隐藏。
/// </summary>
public partial class Overlay : IContentComponent
{
    protected override string? ComponentClass => "s-overlay";

    public RenderFragment? ChildContent { get; set; }

    protected bool OverlayVisible
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            SetDirty();
        }
    }

    [Inject] protected IJSRuntime Js { get; set; } = null!;

    [Parameter]
    public bool Visible
    {
        get;
        set
        {
            field = value;
            OverlayVisible = value;
        }
    }

    [Parameter] public EventCallback<bool> VisibleChanged { get; set; }

    [Parameter] public ElementReference OverlayParent { get; set; }

    protected DomRect? ParentRect
    {
        get;
        set
        {
            if (value is null)
            {
                field = value;
                return;
            }

            if (field is null || !field.Equals(value))
            {
                SetDirty();
            }

            field = value;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        ParentRect = await OverlayParent.GetBoundingClientRect(Js);
    }

    protected void ChangeVisible(bool visible)
    {
        if (OverlayVisible == visible) return;
        OverlayVisible = visible;
        VisibleChanged.InvokeAsync(visible).ConfigureAwait(false);
    }

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);
        if (!OverlayVisible) context.AppendClass("hidden");
        if (ParentRect is not null)
        {
            context.AppendStyle("--ob", ParentRect.Bottom);
            context.AppendStyle("--ot", ParentRect.Top);
            context.AppendStyle("--ol", ParentRect.Left);
            context.AppendStyle("--or", ParentRect.Right);
            context.AppendStyle("--ow", ParentRect.Width);
            context.AppendStyle("--oh", ParentRect.Height);
        }
    }
}