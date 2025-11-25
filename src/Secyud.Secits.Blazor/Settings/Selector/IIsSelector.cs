namespace Secyud.Secits.Blazor.Settings;

public interface IIsSelector<TValue> : IIsPlugin
{
    bool IsItemSelected(TValue value);
    Task ClearActiveItemAsync();
    Task SetActiveItemAsync(TValue value);
    TValue GetActiveItem();
}