using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public abstract partial class SDropdownBase 
{
    private SOverlay? _overlay;
    [Parameter] public SOverlayMode Mode { get; set; } = SOverlayMode.Dynamic;
    [Parameter] public SOverlayHorizontalAlignment HorizontalAlignment { get; set; } = SOverlayHorizontalAlignment.InnerLeft;
    [Parameter] public SOverlayVerticalAlignment VerticalAlignment { get; set; } = SOverlayVerticalAlignment.OuterBottom;
    [Parameter] public int HorizontalInterval { get; set; }
    [Parameter] public int VerticalInterval { get; set; } = 4;
    [Parameter] public SOverlayControlType ControlType { get; set; } = SOverlayControlType.Hover;

    public Task ShowAsync()
    {
        return _overlay?.ShowAsync() ?? Task.CompletedTask;
    }

    public Task HideAsync()
    {
        return _overlay?.HideAsync() ?? Task.CompletedTask;
    }

    protected abstract RenderFragment? GenerateChildContent();
    protected abstract RenderFragment? GenerateDropdownContent();
}