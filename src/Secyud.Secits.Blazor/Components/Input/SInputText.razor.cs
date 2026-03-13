using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Plugins;

namespace Secyud.Secits.Blazor;

public partial class SInputText
{
    [Parameter] public bool Area { get; set; }
    private string? _inputValue;
    private readonly SPluginContainer<ISpInputHandler> _inputHandler = new();

    public override void ApplyPlugin(ISPlugin plugin)
    {
        base.ApplyPlugin(plugin);
        _inputHandler.TryApply(plugin);
    }

    public override void ForgoPlugin(ISPlugin plugin)
    {
        base.ForgoPlugin(plugin);
        _inputHandler.TryForgo(plugin);
    }

    protected override void BuildInputRenderTree(RenderTreeBuilder builder)
    {
        ValidateGeneric();

        builder.OpenElement(0, Area ? "textarea" : "input");

        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttributeIfNotEmpty(2, "name", Name);
        builder.AddAttributeIfNotEmpty(3, "readonly", GetReadonly());
        builder.AddAttributeIfNotEmpty(4, "disabled", GetDisabled());
        builder.AddAttribute(5, "value", _inputValue);
        builder.AddAttribute(6, "oninput", CreateInputEvent(OnInputAsync, _inputValue));
        builder.SetUpdatesAttributeName("value");
        builder.CloseElement();
    }

    protected override bool CheckGenericIsValid()
    {
        var type = GetGenericType();
        return type == typeof(string);
    }

    protected override void OnParametersSet()
    {
        _inputValue = Value;
    }

    protected virtual async Task OnInputAsync(string? str)
    {
        _inputValue = str;
        await _inputHandler.InvokeAsync(
            u => u.HandleInputAsync(str),
            () => TriggerInputChangedEventAsync(str));
    }

    public override async Task TriggerInputChangedEventAsync(string? input)
    {
        await TriggerValueChangedEventAsync(input);
    }
}