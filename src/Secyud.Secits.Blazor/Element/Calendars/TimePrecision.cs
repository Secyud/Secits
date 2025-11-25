namespace Secyud.Secits.Blazor.Element;

public readonly struct TimePrecision
{
    internal TimePrecision(DateTimePrecisionKind precisionKind)
    {
        PrecisionKind = precisionKind;
    }

    public DateTimePrecisionKind PrecisionKind { get; }

    public static implicit operator DateTimePrecision(TimePrecision value)
    {
        return new DateTimePrecision(value.PrecisionKind);
    }

    public bool Disable(DateTimePrecisionKind precisionKind)
    {
        return PrecisionKind switch
        {
            DateTimePrecisionKind.Minute => precisionKind == DateTimePrecisionKind.Second,
            DateTimePrecisionKind.Hour =>
                precisionKind is DateTimePrecisionKind.Second or DateTimePrecisionKind.Minute,
            _ => false
        };
    }

    public static TimePrecision Default => new(DateTimePrecisionKind.Default);
    public static TimePrecision Second => new(DateTimePrecisionKind.Second);
    public static TimePrecision Minute => new(DateTimePrecisionKind.Minute);
    public static TimePrecision Hour => new(DateTimePrecisionKind.Hour);
}