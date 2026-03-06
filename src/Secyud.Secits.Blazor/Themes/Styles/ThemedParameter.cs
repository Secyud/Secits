using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Themes;

public class ThemedParameter : IDirtyParameter
{
    public bool CheckComponentValid(IComponent c)
    {
        return c is IThemedComponent;
    }

    public bool CheckComponentDirty(IComponent c, ParameterView view)
    {
        if (c is IThemedComponent i)
        {
            return !(i.Color == view.GetValueOrDefault<SValue>(nameof(i.Color)));
        }

        return false;
    }

    public void BuildComponentClassStyle(IComponent c, ClassStyleContext context)
    {
        if (c is IThemedComponent i)
        {
            context.AppendClass(i.Color);
        }
    }
}
public class SizedParameter : IDirtyParameter
{
    public bool CheckComponentValid(IComponent c)
    {
        return c is ISizedComponent;
    }

    public bool CheckComponentDirty(IComponent c, ParameterView view)
    {
        if (c is ISizedComponent i)
        {
            return !(i.Size == view.GetValueOrDefault<SValue>(nameof(i.Size)));
        }

        return false;
    }

    public void BuildComponentClassStyle(IComponent c, ClassStyleContext context)
    {
        if (c is ISizedComponent i)
        {
            context.AppendClass(i.Size);
        }
    }
}