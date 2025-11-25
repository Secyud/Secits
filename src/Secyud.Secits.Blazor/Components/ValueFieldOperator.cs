using System.Linq.Expressions;
using System.Reflection;

namespace Secyud.Secits.Blazor;

public class ValueFieldOperator<TValue, TField>
{
    private readonly List<PropertyInfo> _propertyInfos = [];
    private readonly PropertyInfo _lastPropertyInfo;

    public ValueFieldOperator(Expression<Func<TValue, TField>> expression)
    {
        var last = (MemberExpression)expression.Body;
        _lastPropertyInfo = (PropertyInfo)last.Member;
        var queue = new Queue<MemberExpression>();
        queue.Enqueue(last);
        while (queue.Count > 0)
        {
            var me = queue.Dequeue();
            if (me.Expression is MemberExpression next)
            {
                _propertyInfos.Insert(0, (PropertyInfo)next.Member);
                queue.Enqueue(next);
            }
        }
    }

    private object? GetBelongObject(TValue value)
    {
        object? obj = value;
        foreach (var info in _propertyInfos)
        {
            if (obj is null) return null;
            obj = info.GetValue(obj);
        }

        return obj;
    }

    public void SetField(TValue value, TField field)
    {
        var obj = GetBelongObject(value);
        if (obj is null) return;
        _lastPropertyInfo.SetValue(obj, field);
    }

    public TField GetField(TValue value)
    {
        var obj = GetBelongObject(value);
        if (obj is null) return default!;
        return (TField)_lastPropertyInfo.GetValue(obj)!;
    }
}