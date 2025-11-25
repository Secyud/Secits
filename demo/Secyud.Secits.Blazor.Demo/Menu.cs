namespace Secyud.Secits.Blazor;

public static class Menu
{
    public static Dictionary<string, string> Items { get; } = new()
    {
        [nameof(Table)] = Table,
        [nameof(Overview)] = Overview,
        [nameof(Component)] = Component,
    };

    public const string Card = "/card";
    public const string Input = "/input";
    public const string Component = "/component";
    public const string Table = "/table";
    public const string Overview = "/overview";
}