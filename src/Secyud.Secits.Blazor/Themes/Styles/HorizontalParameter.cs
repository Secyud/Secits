using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Themes;

/// <summary>
/// 未使用
/// </summary>
public class HorizontalParameter : IDirtyParameter
{
    public bool CheckComponentValid(IComponent c)
    {
        return c is IHorizontalComponent and not ILayoutedComponent;
    }

    public bool CheckComponentDirty(IComponent c, ParameterView view)
    {
        if (c is IHorizontalComponent i)
        {
            return !(i.Width == view.GetValueOrDefault<SValue>(nameof(i.Width)));
        }

        return false;
    }

    public void BuildComponentClassStyle(IComponent c, ClassStyleContext context)
    {
        if (c is IHorizontalComponent i)
        {
            // 使用前缀类或者style的方式添加
            context.AppendClassOrStyle(i.Width, "w-", "width");
        }
    }
}