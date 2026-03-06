namespace Secyud.Secits.Blazor.Plugins;

public class SpTableColumnInfo
{
    /// <summary>
    /// the sequence for column to display
    /// </summary>
    public int Sequence { get; set; }

    /// <summary>
    /// the visibility of column
    /// </summary>
    public bool Visible { get; set; } = true;

    /// <summary>
    /// the width of column,
    /// null for auto
    /// </summary>
    public double? Width { get; set; }


    public double MinWidth { get; set; } = 50;
    public double MaxWidth { get; set; } = 1200;
}