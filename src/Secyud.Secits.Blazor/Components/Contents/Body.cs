namespace Secyud.Secits.Blazor;

public class Body : AddContentBase
{
    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("body", Class);
    }
}