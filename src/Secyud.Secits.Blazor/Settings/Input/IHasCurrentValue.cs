namespace Secyud.Secits.Blazor.Settings;

public interface IHasCurrentValue<TValue>
{
    TValue CurrentValue { get; set; }
}