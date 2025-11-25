namespace Secyud.Secits.Blazor.Element;

public partial class TimePickerBoardTemplate
{
    protected static readonly int[] Numbers60 = Enumerable.Range(0, 60).ToArray();
    protected static readonly int[] Numbers24 = Enumerable.Range(0, 24).ToArray();

    protected static string Format(int value)
    {
        return value.ToString("00");
    }
}