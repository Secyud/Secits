namespace Secyud.Secits.Blazor;

[SValue]
public partial struct SIconName
{
    public static SIconName Pin => "sis si-pin";
    public static SIconName Bars => "sis si-bars";
    public static SIconName Circle => "sis si-circle";
    public static SIconName Calendar => "sis si-calendar";
    public static SIconName Palette => "sis si-circle";
    public static SIconName Globe => "sis si-circle";


    public static SIconName From(object? obj)
    {
        return new SValue(obj?.ToString() ?? "");
    }
}