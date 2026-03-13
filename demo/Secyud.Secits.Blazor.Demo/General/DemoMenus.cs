using Secyud.Secits.Blazor.Pages;

namespace Secyud.Secits.Blazor;

public static class DemoMenus
{
    public static Dictionary<string, string> Items { get; } = new()
    {
        [nameof(Home)] = Home,
        [nameof(Avatar)] = Avatar,
        [nameof(Button)] = Button,
        [nameof(Input)] = Input,
    };

    public const string Home = "/";
    
    public const string Avatar = "/avatar";
    public const string Button = "/button";

    public const string Input = "/input";
}