using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Themes;

public class ClassStyleParameter : IDirtyParameter
{
    public bool CheckComponentValid(IComponent c)
    {
        return c is IContainerComponent;
    }

    public bool CheckComponentDirty(IComponent c, ParameterView view)
    {
        if (c is IContainerComponent i)
        {
            return !(i.Class == view.GetValueOrDefault<string?>(nameof(i.Class)) &&
                     i.Style == view.GetValueOrDefault<string?>(nameof(i.Style)));
        }

        return false;
    }

    public void BuildComponentClassStyle(IComponent c, ClassStyleContext context)
    {
        if (c is IContainerComponent i)
        {
            context.AppendClass(i.Class);
            context.StyleBuilder.Append(i.Style);
        }
    }
}