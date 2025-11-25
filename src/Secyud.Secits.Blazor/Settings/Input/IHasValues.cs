using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// some input has multiple value.
/// usually for selection.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IHasValues<TValue>
{
    List<TValue> Values { get; set; }

    EventCallback<List<TValue>> ValuesChanged { get; set; }
}