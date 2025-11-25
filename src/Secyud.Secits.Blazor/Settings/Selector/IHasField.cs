using System.Linq.Expressions;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// text field is need to get or set
/// the value of the item.
/// the value field name is also useful.
/// </summary>
public interface IHasField<TValue, TField>
{
    Expression<Func<TValue, TField>>? Field { get; set; }
}