namespace Secyud.Secits.Blazor.Element;

public readonly struct DateTimePrecision
{
    internal DateTimePrecision(DateTimePrecisionKind precisionKind)
    {
        PrecisionKind = precisionKind;
    }

    private DateTimePrecisionKind PrecisionKind { get; }

    public static implicit operator DatePrecision(DateTimePrecision value)
    {
        if (value
            is
            { PrecisionKind: DateTimePrecisionKind.Second
                or DateTimePrecisionKind.Minute
                or DateTimePrecisionKind.Hour })
            return Default;

        return new DatePrecision(value.PrecisionKind);
    }

    public static implicit operator TimePrecision(DateTimePrecision value)
    {
        if (value
            is
            { PrecisionKind: DateTimePrecisionKind.Day
                or DateTimePrecisionKind.Month
                or DateTimePrecisionKind.Year })
            return Default;
        return new TimePrecision(value.PrecisionKind);
    }

    public static DateTimePrecision Default => new(DateTimePrecisionKind.Default);
    public static DateTimePrecision Second => new(DateTimePrecisionKind.Second);
    public static DateTimePrecision Minute => new(DateTimePrecisionKind.Minute);
    public static DateTimePrecision Hour => new(DateTimePrecisionKind.Hour);
    public static DateTimePrecision Day => new(DateTimePrecisionKind.Day);
    public static DateTimePrecision Month => new(DateTimePrecisionKind.Month);
    public static DateTimePrecision Year => new(DateTimePrecisionKind.Year);
}