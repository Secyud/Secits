namespace Secyud.Secits.Blazor.Settings;

public interface IRowStyleProvider<in TItem>
{
    string? GetRowStyle(TItem item);
}