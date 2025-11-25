namespace Secyud.Secits.Blazor;

public class SSelectableContainer(ISelectable value)
{
    public ISelectable Value { get; } = value;
}