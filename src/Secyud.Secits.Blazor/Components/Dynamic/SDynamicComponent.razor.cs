using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

/// <summary>
/// 动态组件，平铺在AppContainer中的组件容器，
/// 用于弹窗，dropdown等组件
/// </summary>
public sealed partial class SDynamicComponent : IDisposable
{
    [Parameter]
    public SDynamicComponentContext? Context
    {
        get;
        set
        {
            if (field == value) return;
            field?.StateHasChangedEvent -= StateHasChanged;
            field = value;
            field?.StateHasChangedEvent += StateHasChanged;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        if (Context?.OnInitializedAsyncEvent is { } onInitializeAsync)
        {
            await onInitializeAsync();
        }
    }

    protected override void OnInitialized()
    {
        if (Context?.OnInitializedEvent is { } onInitialize)
        {
            onInitialize();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (Context?.OnAfterRenderAsyncEvent is { } onAfterRenderAsync)
        {
            await onAfterRenderAsync(firstRender);
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (Context?.OnAfterRenderEvent is { } onAfterRender)
        {
            onAfterRender(firstRender);
        }
    }

    public void Dispose()
    {
        Context = null;
    }
}