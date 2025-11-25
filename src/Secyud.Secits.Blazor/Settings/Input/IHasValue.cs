using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// usually for input. the value can be bind.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IHasValue<TValue>
{
    TValue Value { get; set; }

    EventCallback<TValue> ValueChanged { get; set; }
}