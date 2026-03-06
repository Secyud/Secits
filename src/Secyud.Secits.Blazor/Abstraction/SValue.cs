namespace Secyud.Secits.Blazor;

public readonly struct SValue(string value = "", bool isClass = true) : IEquatable<SValue>
{
    public string Value { get; } = value;
    public bool IsClass { get; } = isClass;
    public bool IsNull { get; } = string.IsNullOrEmpty(value);

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(SValue value)
    {
        return value.ToString();
    }

    public static implicit operator SValue(string str)
    {
        return new SValue(str);
    }

    public static implicit operator SValue(SValue[] values)
    {
        return string.Join(' ', values.Where(u => u is { IsClass: true, IsNull: false }));
    }

    public bool Equals(SValue other)
    {
        return Value == other.Value && IsClass == other.IsClass;
    }

    public override bool Equals(object? obj)
    {
        return obj is SValue other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(SValue left, SValue right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SValue left, SValue right)
    {
        return !(left == right);
    }

    public static SValue Var(string value) => $"var({value})";
    public static SValue Style(string style) => new(style, isClass: false);

    #region UOM

    public static SValue Px(int value) => Style(value + "px");
    public static SValue Rem(double value) => Style(value + "rem");
    public static SValue Em(double value) => Style(value + "em");
    public static SValue P(int value) => Style(value + "%");
    public static SValue Vh(int value) => Style(value + "vh");
    public static SValue Vw(int value) => Style(value + "vw");

    #endregion

    #region Preset

    public static SValue Auto => Style("auto");
    public static SValue FitContent => Style("fit-content");
    public static SValue MaxContent => Style("min-content");
    public static SValue MinContent => Style("max-content");

    #endregion

    #region Global

    public static SValue Inherit => Style("inherit");
    public static SValue Initial => Style("initial");
    public static SValue Unset => Style("unset");

    #endregion
}