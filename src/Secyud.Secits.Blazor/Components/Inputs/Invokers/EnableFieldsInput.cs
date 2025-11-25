using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class EnableFieldsInput<TValue, TField> : EnableValuesInput<TValue>
{
    [Parameter]
    public Func<TValue, TField>? ValueField { get; set; }

    [Parameter]
    public Func<IEnumerable<TField>, Task<IEnumerable<TValue>>>? ItemFinder { get; set; }

    [Parameter]
    public List<TField> Fields { get; set; } = [];

    [Parameter]
    public EventCallback<List<TField>> FieldsChanged { get; set; }

    [Parameter]
    public Expression<Func<List<TField>>>? FieldsExpression { get; set; }

    [Parameter]
    public Func<List<TField>, List<ValidationResult>>? FieldsValidator { get; set; }

    protected override void SetSelectionFromParameter(ParameterContainer parameters)
    {
        base.SetSelectionFromParameter(parameters);
        parameters.UseParameter(Fields, nameof(Fields), async values =>
        {
            if (ItemFinder is not null)
            {
                var items = await ItemFinder.Invoke(values);
                CurrentValuesHash.Clear();
                foreach (var item in items)
                {
                    CurrentValuesHash.Add(item);
                }

                return;
            }

            if (typeof(TValue).IsAssignableFrom(typeof(TField)))
            {
                CurrentValuesHash.Clear();
                foreach (var value in values)
                {
                    if (value is TValue item)
                        CurrentValuesHash.Add(item);
                }
            }
            else
                throw new InvalidOperationException(
                    $"Please set {nameof(ItemFinder)} in {nameof(EnableFieldInput<TValue, TField>)}.");
        });
    }

    protected override async Task OnValueChangedAsync()
    {
        await base.OnValueChangedAsync();
        if (FieldsChanged.HasDelegate)
        {
            var values = CurrentValues.Select(GetValue).ToList();
            await FieldsChanged.InvokeAsync(values);
            await NotifyValidationChangedAsync(FieldsExpression, values);
        }
    }


    protected TField GetValue(TValue item)
    {
        if (ValueField is null)
        {
            if (item is TField value)
                return value;
        }
        else
        {
            return ValueField(item);
        }

        throw new InvalidOperationException("Please set ValueField in EnableValuesInput.");
    }
}