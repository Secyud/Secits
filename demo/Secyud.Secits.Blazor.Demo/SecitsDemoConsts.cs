namespace Secyud.Secits.Blazor;

public static class SecitsDemoConsts
{
    public static Theme[] Themes { get; } =
    [
        Theme.Default, Theme.Primary, Theme.Secondary, Theme.Naive,
        Theme.Success, Theme.Info, Theme.Warning, Theme.Danger
    ];

    public static Size[] Sizes { get; } =
    [
        Size.XSmall, Size.Small, Size.Default, Size.Large, Size.XLarge
    ];
}