using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SInputCheck<TValue>
{
    protected override string ComponentClass => "s-input-check";

    private TValue _currentValue = default!;

    [Parameter] public string? Label { get; set; }

    protected override void BuildInputRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "input");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "type", "checkbox");
        builder.AddAttributeIfNotEmpty(3, "name", Name);
        builder.AddAttribute(4, "value", _currentValue);
        builder.AddAttribute(5, "checked", _currentValue is true or null);
        builder.AddAttribute(6, "indeterminate", _currentValue is null);
        builder.AddAttribute(7, "onchange",
            EventCallback.Factory.CreateBinder(this, OnCheckedChangedAsync, _currentValue));
        builder.SetUpdatesAttributeName("checked");
        builder.CloseElement();
    }

    protected override bool CheckGenericIsValid()
    {
        var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
        return targetType == typeof(bool);
    }

    protected override void OnParametersSet()
    {
        _currentValue = Value;
    }

    protected async Task OnCheckedChangedAsync(TValue value)
    {
        _currentValue = value;
        await TriggerValueChangedEventAsync(value);
    }
}