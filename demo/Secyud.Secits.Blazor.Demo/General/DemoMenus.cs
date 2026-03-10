namespace Secyud.Secits.Blazor;

public static class DemoMenus
{
    public static Dictionary<string, string> Items { get; } = new()
    {
        [nameof(Home)] = Home,
        [nameof(Avatar)] = Avatar,
    };

    public const string Home = "/";
    
    public const string Avatar = "/avatar";

    public const string Input = "/input";
    public const string Component = "/component";
    public const string Table = "/table";
    public const string Overview = "/overview";
    public const string Card = "/card";
}