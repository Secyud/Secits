using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Secyud.Secits.Blazor.Plugins;

public class SpInputValidate<TValue> : SPluginBase, ISpValueHandler<TValue>
{
    [Inject] private IFormValidator Validator { get; set; } = null!;

    /// <summary>
    /// validation context
    /// </summary>
    [CascadingParameter]
    public SValidationContext? ValidationContext { get; set; }

    public override string PluginName => "input-validate";

    public async Task HandleValueAsync(TValue? value)
    {
        if (Context?.Component is EComponentBase<TValue>
            {
                ValueExpression : not null
            } c && ValidationContext is not null)
        {
            var id = FieldIdentifier.Create(c.ValueExpression);
            var context = new ValidationContext(id.Model)
            {
                MemberName = id.FieldName
            };
            var results = await Validator.ValidateValueAsync(context, value);
            ValidationContext.SetValidationResults(this, results);
        }
    }
}