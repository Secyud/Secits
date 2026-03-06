namespace Secyud.Secits.Blazor;

public class DataField(Func<string?> fieldName)
{
    public Func<string?> FieldName { get; } = fieldName;
    public DataSorter Sorter { get; set; } = new();
    public DataFilter Filter { get; set; } = new();
}