using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Themes;

public interface IDirtyParameterProvider
{
    IReadOnlyList<IDirtyParameter> GetDirtyParameters(IComponent component);
}