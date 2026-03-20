using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public static class SComponentExtensions
{
    extension(RenderTreeBuilder builder)
    {
        public void AddAttributeIfNotEmpty(int sequence, string name, string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            builder.AddAttribute(sequence, name, value);
        }
    }

    extension(IComponent component)
    {
        public void AddDirtyParameters(ClassStyleContext context, Lazy<IReadOnlyList<IDirtyParameter>> dirtyParameters)
        {
            foreach (var dirtyParameter in dirtyParameters.Value)
            {
                dirtyParameter.BuildComponentClassStyle(component, context);
            }
        }
    }

    extension(ParameterView parameter)
    {
        public void UseParameter<T>(T previous, string name, Action<T> action)
            where T : struct
        {
            if (parameter.TryGetValue<T>(name, out var value) && !Equals(previous, value))
            {
                action.Invoke(value);
            }
        }
    }
}