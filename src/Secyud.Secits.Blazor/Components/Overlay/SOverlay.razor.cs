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
    [Parameter] public SOverlayMode Mode { get; set; } = SOverlayMode.Static;

    [Parameter]
    public SOverlayHorizontalAlignment HorizontalAlignment
    {
        get;
        set => SetDirty(ref field, value);
    } = SOverlayHorizontalAlignment.Center;

    [Parameter]
    public SOverlayVerticalAlignment VerticalAlignment
    {
        get;
        set => SetDirty(ref field, value);
    } = SOverlayVerticalAlignment.Middle;

    [Parameter]
    public int HorizontalInterval
    {
        get;
        set => SetDirty(ref field, value);
    }

    [Parameter]
    public int VerticalInterval
    {
        get;
        set => SetDirty(ref field, value);
    }


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

        if (OverlayVisible)
        {
            await CreateOverlay();
        }
        else
        {
            await DeleteOverlay();
        }
    }

    protected async Task CreateOverlay()
    {
        if (_visibleChanged && ElementRef is { Context: not null } element &&
            OverlayParent?.ElementRef is { Context: not null } parent)
        {
            await Js.InvokeVoidAsync(SJsModules.Overlay.Create,
                element.Id, element, parent, _closeInvoker.Ref, new SOverlayOptions
                {
                    ControlType = ControlType,
                    HorizontalInterval = HorizontalInterval,
                    VerticalInterval = VerticalInterval,
                });
        }
    }

    protected async Task DeleteOverlay()
    {
        if (_visibleChanged && ElementRef is { Context: not null } element)
        {
            await Js.InvokeVoidAsync(SJsModules.Overlay.Delete, element.Id);
        }
    }

    protected async Task ChangeVisible(bool visible)
    {
        if (OverlayVisible == visible) return;
        OverlayVisible = visible;
        await VisibleChanged.InvokeAsync(visible);
        await InvokeAsync(StateHasChanged);
    }

    public Task ShowAsync()
    {
        return ChangeVisible(true);
    }

    public Task HideAsync()
    {
        return ChangeVisible(false);
    }

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);
        context.AppendClass(HorizontalAlignment);
        context.AppendClass(VerticalAlignment);
        if (!OverlayVisible) context.AppendClass("hidden");
        context.AppendStyle("--ith", HorizontalInterval + "px");
        context.AppendStyle("--itv", VerticalInterval + "px");
    }

    protected async Task DisposeAsync(bool isDisposing)
    {
        if (!isDisposing) return;
        await DeleteOverlay();
    }

    protected override void Dispose(bool isDisposing)
    {
        DisposeAsync(isDisposing).ConfigureAwait(false);
        base.Dispose(isDisposing);
    }
}