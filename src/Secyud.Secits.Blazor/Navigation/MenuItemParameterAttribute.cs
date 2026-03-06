namespace Secyud.Secits.Blazor.Navigation;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class MenuItemParameterAttribute : Attribute
{
    public required string Name { get; set; }
    public int Order { get; set; }
    public string? Format { get; set; }
}