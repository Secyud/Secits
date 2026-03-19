using System.Text.Json.Serialization;

namespace Secyud.Secits.Blazor.JSInterop;

public class DomRect : IEquatable<DomRect>
{
    [JsonPropertyName("bottom")] public double Bottom { get; init; }

    [JsonPropertyName("left")] public double Left { get; init; }

    [JsonPropertyName("height")] public double Height { get; init; }

    [JsonPropertyName("right")] public double Right { get; init; }

    [JsonPropertyName("top")] public double Top { get; init; }

    [JsonPropertyName("width")] public double Width { get; init; }

    [JsonPropertyName("x")] public double X { get; init; }

    [JsonPropertyName("y")] public double Y { get; init; }

    public bool ContainsPoint(double x, double y, double interval = 0)
    {
        return x >= Left - interval && x <= Right + interval && y >= Top - interval && y <= Bottom + interval;
    }

    public bool Equals(DomRect? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Bottom.Equals(other.Bottom) && Left.Equals(other.Left) && Height.Equals(other.Height) &&
               Right.Equals(other.Right) && Top.Equals(other.Top) && Width.Equals(other.Width) && X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DomRect)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Bottom, Left, Height, Right, Top, Width, X, Y);
    }
}