using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Themes;

public interface IDirtyParameter
{
    bool CheckComponentValid(IComponent c);
    bool CheckComponentDirty(IComponent c, ParameterView view);
    void BuildComponentClassStyle(IComponent c, ClassStyleContext context);
}