using Secyud.Secits.Blazor.Pages;

namespace Secyud.Secits.Blazor;

public static class DemoMenus
{
    public static List<(string, string)> Items { get; } = new()
    {
        (nameof(Home), Home),
        (nameof(Avatar), Avatar),
        (nameof(Badge), Badge),
        (nameof(Button), Button),
        (nameof(Form), Form),
        (nameof(Input), Input),
        (nameof(Pager), Pager),
        (nameof(Progress), Progress),
        (nameof(Table), Table),
        (nameof(Tabs), Tabs),
        (nameof(Test), Test),
    };

    public const string Home = "/";

    public const string Avatar = "/avatar";
    public const string Badge = "/Badge";
    public const string Button = "/button";
    public const string Form = "/Form";

    public const string Input = "/input";
    public const string Pager = "/pager";
    public const string Progress = "/progress-bar";
    public const string Table = "/table";
    public const string Test = "/test";
    public const string Tabs = "/tabs";
}