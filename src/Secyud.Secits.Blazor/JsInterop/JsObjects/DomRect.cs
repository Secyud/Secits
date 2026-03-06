using System.Text.Json.Serialization;

namespace Secyud.Secits.Blazor.JSInterop;

public class DomRect
{
    [JsonPropertyName("bottom")]
    public double Bottom { get; set; }

    [JsonPropertyName("left")]
    public double Left { get; set; }

    [JsonPropertyName("height")]
    public double Height { get; set; }

    [JsonPropertyName("right")]
    public double Right { get; set; }

    [JsonPropertyName("top")]
    public double Top { get; set; }

    [JsonPropertyName("width")]
    public double Width { get; set; }

    [JsonPropertyName("x")]
    public double X { get; set; }

    [JsonPropertyName("y")]
    public double Y { get; set; }

    public bool ContainsPoint(double x, double y)
    {
        return x >= Left && x <= Right && y >= Top && y <= Bottom;
    }
}