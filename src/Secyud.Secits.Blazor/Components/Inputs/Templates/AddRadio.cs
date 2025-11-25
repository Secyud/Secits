using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class AddRadio<TValue> : SLayoutPluginBase<SInput<TValue>>, IValueContainer
{
    [Parameter]
    public string? Text { get; set; }

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }


    public Task OnValueUpdatedAsync(object? sender, bool applied)
    {
        return Task.CompletedTask;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var currentValue = Master.InputInvoker.Get()!.GetActiveItem();
        builder.OpenElement(0, "input");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "class", GetClass());
        builder.AddAttribute(3, "style", GetStyle());
        builder.AddAttribute(4, "type", "radio");
        builder.AddAttribute(5, "name", Master.Name);
        builder.AddAttribute(6, "value", BindConverter.FormatValue(Value?.ToString()));
        builder.AddAttribute(7, "checked", Equals(currentValue, Value));
        builder.AddAttribute(8, "onchange", EventCallback.Factory.CreateBinder<string?>(
            this, OnChangeAsync, GetTextField(currentValue)));
        builder.SetUpdatesAttributeName("checked");
        builder.CloseElement();
        if (!string.IsNullOrEmpty(Text))
        {
            builder.OpenElement(9, "label");
            builder.AddContent(10, Text);
            builder.CloseElement();
        }
    }

    protected string? GetTextField(TValue value)
    {
        if (Master.TextField.Get() is { } text)
        {
            return text.ToString(value);
        }

        return value?.ToString();
    }

    protected async Task OnChangeAsync(string? args)
    {
        await Master.InputInvoker.InvokeAsync(invoker
            => invoker.SetActiveItemAsync(this, Value));
    }
}