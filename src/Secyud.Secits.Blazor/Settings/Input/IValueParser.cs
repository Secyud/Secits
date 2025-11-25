namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// for text convert to value.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IValueParser<TValue> : IIsPlugin
{
    bool TryParse(string? str, out TValue output);
}