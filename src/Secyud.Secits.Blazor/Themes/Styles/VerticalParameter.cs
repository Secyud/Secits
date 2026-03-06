using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Themes;

/// <summary>
/// 未使用
/// </summary>
public class VerticalParameter : IDirtyParameter
{
    public bool CheckComponentValid(IComponent c)
    {
        return c is IVerticalComponent and not ILayoutedComponent;
    }

    public bool CheckComponentDirty(IComponent c, ParameterView view)
    {
        if (c is IVerticalComponent i)
        {
            return !(i.Height == view.GetValueOrDefault<SValue>(nameof(i.Height)));
        }

        return false;
    }

    public void BuildComponentClassStyle(IComponent c, ClassStyleContext context)
    {
        if (c is IVerticalComponent i)
        {
            // 使用前缀类或者style的方式添加
            context.AppendClassOrStyle(i.Height, "h-", "height");
        }
    }
}