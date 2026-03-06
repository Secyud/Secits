using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Themes;

public class ActivableParameter : IDirtyParameter
{
    public bool CheckComponentValid(IComponent c)
    {
        return c is IActivableComponent;
    }

    public bool CheckComponentDirty(IComponent c, ParameterView view)
    {
        if (c is IActivableComponent i)
        {
            return i.Disabled != view.GetValueOrDefault<bool>(nameof(i.Disabled));
        }

        return false;
    }

    public void BuildComponentClassStyle(IComponent c, ClassStyleContext context)
    {
        if (c is IActivableComponent { Disabled: true })
            context.AppendClass("disabled");
    }
}