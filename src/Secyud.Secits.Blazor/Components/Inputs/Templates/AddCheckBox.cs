using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class AddCheckBox : SLayoutPluginBase<SInput<bool>>, IValueContainer
{
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Gets the value to be used for the input's "name" attribute.
    /// </summary>
    protected string? NameAttributeValue
    {
        get
        {
            if (AdditionalAttributes?.TryGetValue("name", out var nameAttributeValue) ?? false)
            {
                return Convert.ToString(nameAttributeValue, CultureInfo.InvariantCulture);
            }

            return null;
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "input");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "type", "checkbox");
        builder.AddAttributeIfNotEmpty(3, "name", Master.Name);
        builder.AddAttribute(4, "class", GetClass());
        builder.AddAttribute(5, "style", GetStyle());
        builder.AddAttribute(6, "value", bool.TrueString);
        var currentValue = Master.InputInvoker.Get()?.GetActiveItem() ?? false;
        builder.AddAttribute(7, "checked", BindConverter.FormatValue(currentValue));
        builder.AddAttribute(8, "onchange",
            EventCallback.Factory.CreateBinder<bool>(this, OnCheckedChangedAsync, currentValue));
        builder.SetUpdatesAttributeName("checked");
        builder.CloseElement();
    }

    protected async Task OnCheckedChangedAsync(bool value)
    {
        await Master.InputInvoker.InvokeAsync(u => u.SetActiveItemAsync(this, value));
    }

    public Task OnValueUpdatedAsync(object? sender, bool applied)
    {
        return Task.CompletedTask;
    }
}