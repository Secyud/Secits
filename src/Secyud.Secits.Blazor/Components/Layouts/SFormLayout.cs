using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Validations;

namespace Secyud.Secits.Blazor;

public class SFormLayout : SContentBase
{
    protected override string ComponentName => "form-layout";

    [Parameter]
    public string? Gap { get; set; } = "1rem";

    [Parameter]
    public int? TitleWidth { get; set; }

    [Parameter]
    public bool EnableValidation { get; set; }

    [Parameter]
    public bool ParentValidationForm { get; set; }

    protected override void BuildClassStyle(ClassStyleContext context)
    {
        base.BuildClassStyle(context);
        context.AppendStyle("grid-gap", Gap);
        context.AppendStyle("--label-width", TitleWidth?.ToString());
    }

    protected override void OnBuildRenderTree(RenderTreeBuilder builder)
    {
        if (EnableValidation && !ParentValidationForm)
        {
            builder.OpenComponent<ValidationForm>(-2);
            base.OnBuildRenderTree(builder);
            builder.CloseComponent();
        }
        else
        {
            base.OnBuildRenderTree(builder);
        }
    }
}