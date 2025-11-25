namespace Secyud.Secits.Blazor;

public class SPluggableContainer(IPluggable value)
{
    public IPluggable Value { get; } = value;
}