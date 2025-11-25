namespace Secyud.Secits.Blazor.Settings;
/// <summary>
/// text field is need to get the
/// text of the item.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IHasTextField<TValue>
{
    public Func<TValue, string?>? TextField { get; set; }
}