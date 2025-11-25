using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class EnableValueInput<TValue> : EnableInputDelayInvokerBase<TValue>, IHasCurrentValue<TValue>, IHasValue<TValue>
{
    public TValue CurrentValue
    {
        get => LastActiveItem;
        set => LastActiveItem = value;
    }

    [Parameter]
    public bool EnableNullable { get; set; }

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public Expression<Func<TValue>>? ValueExpression { get; set; }

    protected override void SetSelectionFromParameter(ParameterContainer parameters)
    {
        parameters.UseParameter(Value, nameof(Value), value => { CurrentValue = value; });
    }

    protected override async Task OnValueChangedAsync()
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(CurrentValue);
            await NotifyValidationChangedAsync(ValueExpression, CurrentValue);
        }
    }

    public override bool IsItemSelected(TValue value)
    {
        return Equals(CurrentValue, value);
    }

    protected override async Task OnClearActiveItemAsync()
    {
        CurrentValue = default!;
        await Task.CompletedTask;
    }

    protected override async Task OnSetActiveItemAsync(TValue value)
    {
        if (Equals(CurrentValue, value))
        {
            if (EnableNullable)
            {
                CurrentValue = default!;
            }
        }
        else
        {
            CurrentValue = value;
        }

        await Task.CompletedTask;
    }
}