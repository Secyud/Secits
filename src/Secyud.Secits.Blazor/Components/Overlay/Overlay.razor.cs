using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Secyud.Secits.Blazor.JsInterop;
using Secyud.Secits.Blazor.JSInterop;

namespace Secyud.Secits.Blazor;

public partial class Overlay : IContentComponent
{
    public RenderFragment? ChildContent { get; set; }
    protected bool OverlayVisible { get; set; }

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

    protected DomRect? ParentRect { get; set; }

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

    protected string GetClass()
    {
        var cls = "s-overlay";
        if (!OverlayVisible) cls += " hidden";

        return cls;
    }

    protected string GetStyle()
    {
        if (ParentRect is null) return "";
        return
            $"--ob:{ParentRect.Bottom};" +
            $"--ot:{ParentRect.Top};" +
            $"--ol:{ParentRect.Left};" +
            $"--or:{ParentRect.Right};" +
            $"--ow:{ParentRect.Width};" +
            $"--oh:{ParentRect.Height};"
            ;
    }
}