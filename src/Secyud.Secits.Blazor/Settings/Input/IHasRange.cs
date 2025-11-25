namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// the value has a min max value to limit input.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IHasRange<TValue>
    where TValue : IComparable<TValue>
{
    TValue Max { get; set; }
    TValue Min { get; set; }
}