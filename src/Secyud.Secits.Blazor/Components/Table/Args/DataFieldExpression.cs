using System.Linq.Expressions;
using System.Reflection;

namespace Secyud.Secits.Blazor;

public class DataFieldExpression<TItem, TField>
{
    private readonly string _fieldName;
    private readonly List<PropertyInfo> _propertyInfos = [];
    private readonly PropertyInfo _lastPropertyInfo;

    public DataFieldExpression(Expression<Func<TItem, TField>> expression)
    {
        var nameList = new List<string>();
        var last = (MemberExpression)expression.Body;
        _lastPropertyInfo = (PropertyInfo)last.Member;
        nameList.Add(last.Member.Name);
        var queue = new Queue<MemberExpression>();
        queue.Enqueue(last);
        while (queue.Count > 0)
        {
            var me = queue.Dequeue();
            if (me.Expression is MemberExpression next)
            {
                _propertyInfos.Insert(0, (PropertyInfo)next.Member);
                nameList.Insert(0, me.Member.Name);
                queue.Enqueue(next);
            }
        }

        _fieldName = string.Join(".", nameList);
    }

    private object? GetBelongObject(TItem value)
    {
        object? obj = value;
        foreach (var info in _propertyInfos)
        {
            if (obj is null) return null;
            obj = info.GetValue(obj);
        }

        return obj;
    }

    public void SetField(TItem value, object? field)
    {
        var obj = GetBelongObject(value);
        if (obj is null) return;
        _lastPropertyInfo.SetValue(obj, field);
    }

    public object? GetField(TItem value)
    {
        var obj = GetBelongObject(value);
        if (obj is null) return default(TField);
        return _lastPropertyInfo.GetValue(obj);
    }

    public string GetFieldName()
    {
        return _fieldName;
    }

    public Type GetFieldType()
    {
        return typeof(TField);
    }
}