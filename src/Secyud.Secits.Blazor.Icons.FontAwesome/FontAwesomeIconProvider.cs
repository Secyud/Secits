using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor;

public class FontAwesomeIconProvider : IIconProvider
{
    private readonly Dictionary<IconName, string> _fontAwesomeIcons = new()
    {
        [IconName.None] = "",
        [IconName.Create] = "fas fa-file",
        [IconName.Update] = "fas fa-pen-to-square",
        [IconName.Delete] = "fas fa-trash",
        [IconName.Search] = "fas fa-magnifying-glass",
        [IconName.Cross] = "fas fa-xmark",
        [IconName.CaretDown] = "fas fa-caret-down",
        [IconName.CaretUp] = "fas fa-caret-up",
        [IconName.LeftAngles] = "fas fa-angles-left",
        [IconName.LeftAngle] = "fas fa-angle-left",
        [IconName.RightAngle] = "fas fa-angle-right",
        [IconName.RightAngles] = "fas fa-angles-right",
        [IconName.UpAngle] = "fas fa-angle-up",
        [IconName.DownAngle] = "fas fa-angle-down",
        [IconName.UpAngles] = "fas fa-angles-up",
        [IconName.DownAngles] = "fas fa-angles-down",
        [IconName.Pin] = "fas fa-thumbtack",
        [IconName.Bars] = "fas fa-bars",
        [IconName.Exclamation] = "fas fa-exclamation",
        [IconName.Laptop] = "fas fa-laptop",
        [IconName.Question] = "fas fa-question",
        [IconName.Check] = "fas fa-check",
        [IconName.Refresh] = "fas fa-rotate-right",
        [IconName.Palette] = "fas fa-palette",
        [IconName.Globe] = "fas fa-globe",
        [IconName.Lock] = "fas fa-lock",
        [IconName.Random] = "fas fa-dice",
    };

    public string? GetIcon(IconName name)
    {
        return _fontAwesomeIcons.GetValueOrDefault(name) ;
    }
}