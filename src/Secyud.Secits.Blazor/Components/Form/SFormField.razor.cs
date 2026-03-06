using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SFormField : IContentComponent, IDisposable
{
    protected override string? ComponentClass => "s-form-field";
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Validate { get; set; } = true;
    public SValidationContext Context { get; set; } = new();

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public RenderFragment? TitleTemplate { get; set; }

    /// <summary>
    /// <see cref="SFormSpan"/>
    /// </summary>
    [Parameter]
    public SFormSpan Span { get; set; }

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