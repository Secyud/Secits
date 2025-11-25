using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract class SSelectorPluginBase<TComponent, TContainer, TValue> : SPluginBase<TComponent>,
    IIsSelector<TValue> where TComponent : class, IPluggable where TContainer : class
{
    private TContainer? _selectable;

    protected TContainer Selectable =>
        _selectable ?? throw new InvalidOperationException(
            "Inputs need a invoker to handle value callback");

    protected bool SelectableValid => _selectable is not null;

    [CascadingParameter]
    public SSelectableContainer? SelectableComponent
    {
        set
        {
            if (_selectable == value?.Value) return;
            if (_selectable is not null)
            {
                ApplySelectable();
                StateHasChanged();
            }

            _selectable = value?.Value as TContainer;
            if (_selectable is not null)
            {
                ForgoSelectable();
                StateHasChanged();
            }
        }
    }

    protected virtual void ApplySelectable()
    {
    }

    protected virtual void ForgoSelectable()
    {
    }

    public abstract bool IsItemSelected(TValue value);
    public abstract Task ClearActiveItemAsync();
    public abstract Task SetActiveItemAsync( TValue value);
    public abstract TValue GetActiveItem();
}