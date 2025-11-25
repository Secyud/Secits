namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// settings may have different way to sync value.
/// for special use.
/// </summary>
public interface IValueContainer : IIsPlugin
{
    Task OnValueUpdatedAsync(object? sender, bool applied);
}