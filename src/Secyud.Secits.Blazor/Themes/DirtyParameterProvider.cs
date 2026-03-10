using System.Collections.Concurrent;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace Secyud.Secits.Blazor.Themes;

public class DirtyParameterProvider(IOptions<SecitsOptions> options) : IDirtyParameterProvider
{
    private readonly ConcurrentDictionary<Type, List<IDirtyParameter>> _parameters = [];

    public IReadOnlyList<IDirtyParameter> GetDirtyParameters(IComponent component)
    {
        var type = component.GetType();
        if (!_parameters.TryGetValue(type, out var list))
        {
            list = options.Value.Parameters
                .Where(parameter => parameter.CheckComponentValid(component)).ToList();

            _parameters[type] = list;
        }

        return list;
    }
}