using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class EnableFieldInput<TValue, TField> : EnableValueInput<TValue>
{
    [Parameter]
    public Func<TValue, TField>? ValueField { get; set; }

    [Parameter]
    public Func<TField, Task<TValue>>? ItemFinder { get; set; }

    [Parameter]
    public TField Field { get; set; } = default!;

    [Parameter]
    public EventCallback<TField> FieldChanged { get; set; }

    [Parameter]
    public Expression<Func<TField>>? FieldExpression { get; set; }

    [Parameter]
    public Func<TField, List<ValidationResult>>? FieldValidator { get; set; }


    protected override void SetSelectionFromParameter(ParameterContainer parameters)
    {
        base.SetSelectionFromParameter(parameters);
        parameters.UseParameter(Field, nameof(Field), async field =>
        {
            if (ItemFinder is not null)
            {
                CurrentValue = await ItemFinder.Invoke(field);
                return;
            }

            if (field is TValue item)
                CurrentValue = item;
            else
                throw new InvalidOperationException(
                    $"Please set {nameof(ItemFinder)} in {nameof(EnableFieldInput<TValue, TField>)}.");
        });
    }

    protected override async Task OnValueChangedAsync()
    {
        await base.OnValueChangedAsync();
        var value = CurrentValue is null ? default! : GetValue(CurrentValue);
        if (FieldChanged.HasDelegate)
        {
            await FieldChanged.InvokeAsync(value);
            await NotifyValidationChangedAsync(FieldExpression, value);
        }
    }

    protected TField GetValue(TValue item)
    {
        if (ValueField is not null)
            return ValueField(item);

        if (item is TField value) return value;

        throw new InvalidOperationException(
            $"Please set {nameof(ValueField)} in {nameof(EnableFieldInput<TValue, TField>)}.");
    }
}