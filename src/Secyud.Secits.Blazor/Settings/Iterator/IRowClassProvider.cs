namespace Secyud.Secits.Blazor.Settings;

public interface IRowClassProvider<in TItem>
{
    string? GetRowClass(TItem item);
}