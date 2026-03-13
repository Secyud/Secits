using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using Secyud.Secits.Blazor.JsInterop;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SInputCheck<TValue>
{
    protected override string ComponentClass => "s-input-check";

    private TValue _currentValue = default!;
    private ElementReference _input;

    [Inject] protected IJSRuntime Js { get; set; } = null!;
    [Parameter] public string? Label { get; set; }

    protected override void BuildInputRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "input");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "type", "checkbox");
        builder.AddAttributeIfNotEmpty(3, "name", Name);
        builder.AddAttribute(4, "value", _currentValue);
        builder.AddAttribute(5, "checked", _currentValue is true);
        builder.AddAttribute(6, "onchange",
            EventCallback.Factory.CreateBinder(this, OnCheckedChangedAsync, _currentValue));
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
        if (!Equals(_currentValue, Value))
        {
            _currentValue = Value;
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
        return _input.SetPropertyAsync(Js, "indeterminate", _currentValue is null);
    }


    protected async Task OnCheckedChangedAsync(TValue value)
    {
        _currentValue = value;
        await TriggerValueChangedEventAsync(value);
    }
}