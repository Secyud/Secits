namespace Secyud.Secits.Blazor.Settings;

public interface ITreeItem<TItem>
    where TItem : ITreeItem<TItem>
{
    int Depth { get; set; }
    bool IsExpended { get; set; }
    List<TItem>? Children { get; }
}