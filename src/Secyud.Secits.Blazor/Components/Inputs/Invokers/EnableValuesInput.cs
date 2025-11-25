using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class EnableValuesInput<TValue> : EnableInputDelayInvokerBase<TValue>, IHasCurrentValues<TValue>,
    IHasValues<TValue>
{
    protected readonly HashSet<TValue> CurrentValuesHash = [];

    public IEnumerable<TValue> CurrentValues => CurrentValuesHash;

    [Parameter]
    public List<TValue> Values { get; set; } = [];

    [Parameter]
    public EventCallback<List<TValue>> ValuesChanged { get; set; }

    [Parameter]
    public Expression<Func<List<TValue>>>? ValuesExpression { get; set; }

    [Parameter]
    public Func<List<TValue>, List<ValidationResult>>? ValuesValidator { get; set; }

    protected override void SetSelectionFromParameter(ParameterContainer parameters)
    {
        parameters.UseParameter(Values, nameof(Values), value =>
        {
            CurrentValuesHash.Clear();
            foreach (var v in value)
            {
                CurrentValuesHash.Add(v);
            }
        });
    }

    protected override async Task OnValueChangedAsync()
    {
        if (ValuesChanged.HasDelegate)
        {
            await ValuesChanged.InvokeAsync(CurrentValues.ToList());
            await NotifyValidationChangedAsync(ValuesExpression, CurrentValues);
        }
    }

    protected override async Task OnClearActiveItemAsync()
    {
        CurrentValuesHash.Clear();
        await Task.CompletedTask;
    }

    public override bool IsItemSelected(TValue value)
    {
        return value is not null && CurrentValues.Contains(value);
    }

    protected override async Task OnSetActiveItemAsync(TValue value)
    {
        LastActiveItem = value;
        if (!CurrentValuesHash.Remove(value))
            CurrentValuesHash.Add(value);
        await Task.CompletedTask;
    }
}