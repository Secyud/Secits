using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class EnableValueTextField<TValue> : SPluginBase<SInput<TValue>>, IValueTextField<TValue>, IHasTextField<TValue>
{
    [Parameter]
    public Func<TValue, string?>? TextField { get; set; }

    protected override void ApplySetting()
    {
        Master.TextField.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.TextField.Forgo(this);
    }

    public string? ToString(TValue value)
    {
        return TextField?.Invoke(value) ?? value?.ToString();
    }
}