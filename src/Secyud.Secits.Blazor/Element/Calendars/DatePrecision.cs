namespace Secyud.Secits.Blazor.Element;

public readonly struct DatePrecision
{
    internal DatePrecision(DateTimePrecisionKind precisionKind)
    {
        PrecisionKind = precisionKind;
    }

    private DateTimePrecisionKind PrecisionKind { get; }

    public static implicit operator DateTimePrecision(DatePrecision value)
    {
        return new DateTimePrecision(value.PrecisionKind);
    }

    public static DatePrecision Default => new(DateTimePrecisionKind.Default);
    public static DatePrecision Day => new(DateTimePrecisionKind.Day);
    public static DatePrecision Month => new(DateTimePrecisionKind.Month);
    public static DatePrecision Year => new(DateTimePrecisionKind.Year);
}