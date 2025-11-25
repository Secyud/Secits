using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public partial class SRoller<TValue> : ICanActive, IHasTextField<TValue>
{
    [Parameter, Range(1, 4)]
    public int NumberCount { get; set; } = 1;

    [Parameter]
    public Func<TValue, string?>? TextField { get; set; }

    [Parameter]
    public IList<TValue>? Items { get; set; }

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public bool Cycle { get; set; }

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? Class { get; set; }

    protected int CurrentIndex;
    protected TValue CurrentValue = default!;
    protected int MaxIndex;

    protected bool ComponentInvalid => Readonly || Disabled || MaxIndex >= 0;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        if (!Equals(CurrentValue, Value))
        {
            CurrentValue = Value;
            CurrentIndex = Items?.IndexOf(Value) ?? 0;
        }

        MaxIndex = (Items?.Count ?? 0) - 1;
    }

    private async Task OnWheelAsync(WheelEventArgs args)
    {
        if (ComponentInvalid) return;
        var max = MaxIndex;
        var index = (int)args.DeltaY / 80 + CurrentIndex;
        index = Cycle ? GetCycleIndex(index) : Math.Min(Math.Max(0, index), max);
        await OnIndexChangedAsync(index);
    }

    private async Task OnIndexChangedAsync(int index)
    {
        if (ComponentInvalid) return;
        Value = Items![index];
        await ValueChanged.InvokeAsync(Value);
        await InvokeAsync(StateHasChanged);
    }

    protected int GetCycleIndex(int index)
    {
        if (ComponentInvalid) return 0;
        var modValue = MaxIndex + 1;
        index = (index + modValue) % modValue;
        return index;
    }
}