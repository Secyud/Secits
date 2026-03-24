using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SFormField : IContentComponent, IDisposable
{
    protected override string? ComponentClass => "s-form-field";
    protected SValidationContext Context { get; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Validate { get; set; } = true;
    [Parameter] public string? Title { get; set; }

    [Parameter] public RenderFragment? TitleTemplate { get; set; }

    /// <summary>
    /// <see cref="SFormSpan"/>
    /// </summary>
    [Parameter]
    public SFormSpan Span { get; set; } = SFormSpan.Is3;

    [CascadingParameter]
    protected SValidationGroupContext? GroupContext
    {
        get;
        set
        {
            if (field == value) return;
            field?.FieldContexts.Remove(Context);
            field = value;
            field?.FieldContexts.Add(Context);
        }
    }

    public void Dispose()
    {
        GroupContext = null;
    }

    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);
        context.AppendClass(Span);
    }
}