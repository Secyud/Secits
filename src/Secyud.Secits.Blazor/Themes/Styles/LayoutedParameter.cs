using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Themes;

public class LayoutedParameter : IDirtyParameter
{
    public bool CheckComponentValid(IComponent c)
    {
        return c is ILayoutedComponent;
    }

    public bool CheckComponentDirty(IComponent c, ParameterView view)
    {
        if (c is ILayoutedComponent i)
        {
            return !(i.Height == view.GetValueOrDefault<SValue>(nameof(i.Height)) &&
                     i.Width == view.GetValueOrDefault<SValue>(nameof(i.Width)));
        }

        return false;
    }

    public void BuildComponentClassStyle(IComponent c, ClassStyleContext context)
    {
        if (c is ILayoutedComponent i)
        {
            // 使用前缀类或者style的方式添加
            context.AppendClassOrStyle(i.Height, "h-", "height");
            context.AppendClassOrStyle(i.Width, "w-", "width");
        }
    }
}