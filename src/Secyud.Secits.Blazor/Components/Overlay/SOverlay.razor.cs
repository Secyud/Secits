using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Secyud.Secits.Blazor.JsInterop;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

/// <summary>
/// 浮在所有组件上方的组件，可以控制显示隐藏。
/// </summary>
public partial class SOverlay : IContentComponent
{
    private readonly JsInvoker _closeInvoker;
    private bool _visibleChanged;

    public SOverlay()
    {
        _closeInvoker = new JsInvoker(() => ChangeVisible(false));
    }

    protected override string ComponentClass => "s-overlay";

    [Inject] protected IJSRuntime Js { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public int Interval { get; set; } = 4;

    [Parameter]
    public SOverlayAlignment OverlayAlignment
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            SetDirty();
        }
    } = SOverlayAlignment.Bottom;

    [Parameter]
    public SOverlayJustify OverlayJustify
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            SetDirty();
        }
    } = SOverlayJustify.Begin;

    [Parameter] public SOverlayMode Mode { get; set; } = SOverlayMode.Static;

    protected bool OverlayVisible
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            _visibleChanged = true;
            StateHasChanged();
        }
    }

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

    [Parameter] public IElementComponent? OverlayParent { get; set; }
    [Parameter] public SOverlayControlType ControlType { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_visibleChanged && ElementRef is { Context: not null } element &&
            OverlayParent?.ElementRef is { Context: not null } parent)
        {
            if (OverlayVisible)
            {
                await Js.InvokeVoidAsync(SJsModules.Overlay.Create,
                    element.Id, element, parent, ControlType.ToString(), _closeInvoker.Ref);
            }
            else
            {
                await Js.InvokeVoidAsync(SJsModules.Overlay.Delete, element.Id);
            }
        }
    }

    protected async Task ChangeVisible(bool visible)
    {
        if (OverlayVisible == visible) return;
        OverlayVisible = visible;
        await VisibleChanged.InvokeAsync(visible);
        await InvokeAsync(StateHasChanged);
    }

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);
        context.AppendClass(OverlayAlignment);
        context.AppendClass(OverlayJustify);
        if (!OverlayVisible) context.AppendClass("hidden");
    }
}