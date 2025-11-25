namespace Secyud.Secits.Blazor.Settings;

public interface IValueTextField<in TValue> : IIsPlugin
{
    string? ToString(TValue value);
}