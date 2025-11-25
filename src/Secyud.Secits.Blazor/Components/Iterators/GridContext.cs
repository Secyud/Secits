using System.Text;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class GridContext<TValue>
{
    public GridColumnListContext<TValue> FrontColumns { get; } = new();
    public GridColumnListContext<TValue> MiddleColumns { get; } = new();

    public GridColumnListContext<TValue> BehindColumns { get; } = new()
    {
        MinSequence = int.MaxValue,
        MaxSequence = int.MaxValue
    };

    public GridContext(SGrid<TValue> grid)
    {
        for (var i = 0; i < grid.TableColumns.Count; i++)
        {
            var column = grid.TableColumns[i];
            var context = new GridColumnContext<TValue>(column, i);

            switch (column.Position)
            {
                case ColumnPosition.Front:
                {
                    FrontColumns.AddColumn(context);
                    break;
                }
                case ColumnPosition.Middle:
                {
                    MiddleColumns.AddColumn(context);
                    break;
                }
                case ColumnPosition.Behind:
                {
                    BehindColumns.AddColumn(context);
                    break;
                }
            }

            if (FrontColumns.MaxSequence > MiddleColumns.MinSequence)
            {
                throw new InvalidOperationException("Front columns should set at begin.");
            }

            if (BehindColumns.MinSequence < MiddleColumns.MaxSequence)
            {
                throw new InvalidOperationException("Behind columns should set at last.");
            }
        }
    }

    public string GetBodyTemplate()
    {
        var sb = new StringBuilder();

        if (FrontColumns.Width > 0)
        {
            sb.Append($" {FrontColumns.Width}px");
        }

        foreach (var column in MiddleColumns.Columns)
        {
            sb.Append($" {column.Renderer.RealWidth}px");
        }

        if (BehindColumns.Width > 0)
        {
            sb.Append($" {BehindColumns.Width}px");
        }

        return sb.ToString();
    }

    public string GetFrontTemplate()
    {
        return string.Join(' ', FrontColumns.Columns.Select(u => $"{u.Renderer.RealWidth}px"));
    }

    public string GetBehindTemplate()
    {
        return string.Join(' ', BehindColumns.Columns.Select(u => $"{u.Renderer.RealWidth}px"));
    }

    public string GetGridWidth()
    {
        return $"{FrontColumns.Width + MiddleColumns.Width + BehindColumns.Width}px";
    }
}