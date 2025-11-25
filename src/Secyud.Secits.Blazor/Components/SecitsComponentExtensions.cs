using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor;

internal static class SecitsComponentExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddAttributeIf(this RenderTreeBuilder builder, bool condition, int sequence, string name, object? value)
    {
        if (!condition)
        {
            builder.AddAttribute(sequence, name, value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddAttributeIfNotEmpty(this RenderTreeBuilder builder, int sequence, string name, string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            builder.AddAttribute(sequence, name, value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddActiveAttribute<TComponent>(this RenderTreeBuilder builder, int sequence, SPluginBase<TComponent> component)
        where TComponent : class, IPluggable, ICanActive
    {
        if (component.Master.Disabled)
        {
            builder.AddAttribute(sequence, "disabled", "disabled");
        }
        else if (component.Master.Readonly)
        {
            builder.AddAttribute(sequence, "readonly", "readonly");
        }
    }
}