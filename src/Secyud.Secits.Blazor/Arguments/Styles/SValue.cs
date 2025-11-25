namespace Secyud.Secits.Blazor;

public readonly struct SValue(string value, bool isClass = false) : IEquatable<SValue>, IMaybeClassParameter
{
    public string Value { get; } = value;
    public bool IsClass { get; } = isClass;

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
        if (str.Length != 0 && str[0] == '.')
        {
            return new SValue(str[1..], true);
        }

        return new SValue(str);
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
    public static SValue Class(string cls) => new(cls, true);

    #region UOM

    public static SValue Px(int value) => value + "px";
    public static SValue Rem(double value) => value + "rem";
    public static SValue Em(double value) => value + "em";
    public static SValue P(int value) => value + "%";
    public static SValue Vh(int value) => value + "vh";
    public static SValue Vw(int value) => value + "vw";

    #endregion

    #region Preset

    public static SValue Auto => "auto";
    public static SValue FitContent => "fit-content";
    public static SValue MaxContent => "min-content";
    public static SValue MinContent => "max-content";

    #endregion

    #region Global

    public static SValue Inherit => "inherit";
    public static SValue Initial => "initial";
    public static SValue Unset => "unset";

    #endregion
}