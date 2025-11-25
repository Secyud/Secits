namespace Secyud.Secits.Blazor.Settings;

public interface IHasCurrentValues<out TValue>
{
    IEnumerable<TValue> CurrentValues { get;  }
}