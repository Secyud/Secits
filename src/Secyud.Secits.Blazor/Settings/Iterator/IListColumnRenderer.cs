using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

public interface IListColumnRenderer<in TItem> : IIsPlugin
{
    RenderFragment GenerateColumn(TItem item);
}