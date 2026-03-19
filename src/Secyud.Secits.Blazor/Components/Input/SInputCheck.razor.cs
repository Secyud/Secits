using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using Secyud.Secits.Blazor.JsInterop;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SInputCheck<TValue>
{
    protected override string ComponentClass => "s-input-check";

    private ElementReference _input;

    [Inject] protected IJSRuntime Js { get; set; } = null!;
    [Parameter] public string? Label { get; set; }

    protected override void BuildInputRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "input");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "type", "checkbox");
        builder.AddAttributeIfNotEmpty(3, "name", Name);
        builder.AddAttribute(4, "value", CurrentValue);
        builder.AddAttribute(5, "checked", CurrentValue is true);
        builder.AddAttribute(6, "onchange",
            EventCallback.Factory.CreateBinder(this, OnCheckedChangedAsync, CurrentValue));
        builder.SetUpdatesAttributeName("checked");
        builder.AddElementReferenceCapture(7, e => _input = e);
        builder.CloseElement();
    }

    protected override bool CheckGenericIsValid()
    {
        var targetType = GetGenericType();
        return targetType == typeof(bool);
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        if (!Equals(CurrentValue, Value))
        {
            CurrentValue = Value;
            await SyncIndeterminateStateAsync();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SyncIndeterminateStateAsync();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    protected ValueTask SyncIndeterminateStateAsync()
    {
        return _input.SetProperty(Js, "indeterminate", CurrentValue is null);
    }


    protected async Task OnCheckedChangedAsync(TValue value)
    {
        CurrentValue = value;
        await TriggerValueChangedEventAsync(value);
    }
}