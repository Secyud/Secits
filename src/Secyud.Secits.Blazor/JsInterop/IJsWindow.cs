namespace Secyud.Secits.Blazor.JSInterop;

public interface IJsWindow
{
    ValueTask Open(string? uri = null, string? target = null, string? windowFeatures = null);
    ValueTask OpenOnBlank(string? uri);
    ValueTask Alert(string? message = null);
    ValueTask AToB(string? str = null);
    ValueTask BToA(string? str = null);
    ValueTask Close();
    ValueTask Confirm(string? message = null);
    ValueTask Focus();
}