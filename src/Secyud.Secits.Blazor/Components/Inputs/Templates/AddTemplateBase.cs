using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract class AddTemplateBase<TValue> : SActivablePluginBase<SInput<TValue>>, IValueContainer
{
    protected virtual string? InputString => ParsingFailed ? ValueString : CurrentString;
    protected string? CurrentString { get; set; }
    protected string? ValueString { get; set; }
    protected bool ParsingFailed { get; set; }
    protected TValue CachedValue { get; set; } = default!;

    [Parameter]
    public bool SubmitOnInput { get; set; }

    [Parameter]
    public string? Format { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }

    public async Task OnValueUpdatedAsync(object? sender, bool applied)
    {
        if (sender == this) return;
        if (Master.InputInvoker.Get() is { } invoker)
            CachedValue = invoker.GetActiveItem();
        else
            CachedValue = default!;
        ValueString = FormatValueAsString(CachedValue);
        CurrentString = ValueString;
        ParsingFailed = false;
        await Task.CompletedTask;
    }

    protected async Task OnInputAsync(string? args)
    {
        await OnInputInvokeAsync(args, SubmitOnInput);
    }

    protected async Task OnChangeAsync(string? args)
    {
        await OnInputInvokeAsync(args);
    }

    protected virtual Task OnInputStringHandleAsync(string? str)
    {
        CurrentString = str;
        return Task.CompletedTask;
    }

    protected virtual Task OnInputValueHandleAsync()
    {
        switch (CurrentString)
        {
            case null:
                CachedValue = default!;
                ParsingFailed = false;
                break;
            case TValue value:
                CachedValue = value;
                ParsingFailed = false;
                break;
            default:
                if (Master.ValueConverter.Get() is { } converter)
                {
                    ParsingFailed = !converter.TryParse(CurrentString, out var value);
                    if (!ParsingFailed) CachedValue = value;
                }
                else
                {
                    ParsingFailed = !TryParseValueFromString(CurrentString, out var value);
                    CachedValue = value ?? default!;
                }

                break;
        }

        var invoker = Master.InputInvoker.Get();
        if (invoker is not null)
        {
            var currentItem = invoker.GetActiveItem();

            var textField = Master.TextField.Get();
            ValueString = textField is null
                ? currentItem?.ToString()
                : textField.ToString(currentItem);
        }

        return Task.CompletedTask;
    }

    protected virtual async Task OnInputInvokeAsync(string? value, bool submit = true)
    {
        if (Master.Readonly || Master.Disabled) return;
        await OnInputStringHandleAsync(value);
        await OnInputValueHandleAsync();
        if (submit && Master.InputInvoker.Get() is { } invoker)
        {
            await invoker.SetActiveItemAsync(this, CachedValue);
        }
    }

    protected override void ApplySetting()
    {
        base.ApplySetting();
        Master.ValueContainer.Apply(this);
        OnValueUpdatedAsync(null!, true).ConfigureAwait(false);
    }

    protected override void ForgoSetting()
    {
        base.ForgoSetting();
        Master.ValueContainer.Forgo(this);
    }

    protected string? GetReadonly()
    {
        return Master.Readonly || Readonly ? "readonly" : null;
    }

    protected string? GetDisabled()
    {
        return Master.Disabled || Disabled ? "disabled" : null;
    }

    protected virtual string? FormatValueAsString(TValue? value)
    {
        return value?.ToString();
    }

    protected abstract bool TryParseValueFromString(string? value,
        [MaybeNullWhen(false)] out TValue result);
}