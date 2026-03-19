using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Secyud.Secits.Blazor.JsInterop;
using Secyud.Secits.Blazor.JSInterop;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

/// <summary>
/// 浮在所有组件上方的组件，可以控制显示隐藏。
/// </summary>
public partial class SOverlay : IContentComponent
{
    protected override string ComponentClass => "s-overlay";

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
            SetDirty();
            CheckEventAsync().ConfigureAwait(false);
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

    [Parameter] public IElementComponent? OverlayParent { get; set; }
    [Parameter] public SOverlayControlType ControlType { get; set; }
    protected JsEvent<string, MouseEventArgs> DomClickEvent { get; set; } = null!;
    protected JsEvent<string, MouseEventArgs> DomMoveEvent { get; set; } = null!;
    protected JsEvent<string, MouseEventArgs> DomWheelEvent { get; set; } = null!;

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
                field = value;
                SetDirty();
                StateHasChanged();
            }
            else
            {
                field = value;
            }
        }
    }

    protected override void OnInitialized()
    {
        DomClickEvent = new JsEvent<string, MouseEventArgs>("document", "click", Js);
        DomMoveEvent = new JsEvent<string, MouseEventArgs>("document", "mousemove", Js);
        DomWheelEvent = new JsEvent<string, MouseEventArgs>("document", "wheel", Js, 100);
        DomClickEvent.Event += OnDocumentCheck;
        DomMoveEvent.Event += OnDocumentCheck;
        DomWheelEvent.Event += OnDocumentWheel;
    }

    /// <summary>
    /// 在弹出或关闭时，注册或注销事件
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    protected async Task CheckEventAsync()
    {
        if (OverlayVisible)
        {
            switch (ControlType)
            {
                case SOverlayControlType.Hover:
                    await DomMoveEvent.CreateEventAsync();
                    break;
                case SOverlayControlType.Click:
                    await DomClickEvent.CreateEventAsync();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            await DomWheelEvent.CreateEventAsync();
            if (OverlayParent?.ElementRef is { Context : not null } element)
            {
                ParentRect = await element.GetBoundingClientRect(Js);
            }
        }
        else
        {
            await DomMoveEvent.DeleteEventAsync();
            await DomClickEvent.DeleteEventAsync();
            await DomWheelEvent.DeleteEventAsync();
        }

        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// 处理doc检查，不在父级元素或本元素的将关闭。
    /// </summary>
    /// <param name="args"></param>
    protected async Task OnDocumentWheel(MouseEventArgs args)
    {
        if (OverlayParent?.ElementRef is { Context : not null } element)
        {
            ParentRect = await element.GetBoundingClientRect(Js);
        }

        await InvokeAsync(StateHasChanged);
    }

    /// <summary>
    /// 处理doc检查，不在父级元素或本元素的将关闭。
    /// </summary>
    /// <param name="args"></param>
    protected async Task OnDocumentCheck(MouseEventArgs args)
    {
        if (ParentRect is null || ElementRef.Context is null) return;
        if (ParentRect.ContainsPoint(args.ClientX, args.ClientY, Interval)) return;
        var rect = await ElementRef.GetBoundingClientRect(Js);
        if (rect.ContainsPoint(args.ClientX, args.ClientY, Interval)) return;
        await ChangeVisible(false);
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
        if (ParentRect is not null)
        {
            context.AppendStyle("--ob", $"{ParentRect.Bottom - Interval:F}px");
            context.AppendStyle("--ot", $"{ParentRect.Top - Interval:F}px");
            context.AppendStyle("--ol", $"{ParentRect.Left - Interval:F}px");
            context.AppendStyle("--or", $"{ParentRect.Right - Interval:F}px");
            context.AppendStyle("--ow", $"{ParentRect.Width + 2 * Interval:F}px");
            context.AppendStyle("--oh", $"{ParentRect.Height + 2 * Interval:F}px");
            context.AppendStyle("--oi", $"{Interval:F}px");
        }
    }

    protected async Task DisposeAsync()
    {
        await DomClickEvent.DisposeAsync();
        await DomMoveEvent.DisposeAsync();
    }

    protected override void Dispose(bool isDisposing)
    {
        if (isDisposing)
        {
            DisposeAsync().ConfigureAwait(false);
        }

        base.Dispose(isDisposing);
    }
}