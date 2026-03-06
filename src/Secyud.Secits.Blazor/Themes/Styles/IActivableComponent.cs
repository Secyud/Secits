namespace Secyud.Secits.Blazor.Themes;

/// <summary>
/// 可激活控件
/// </summary>
public interface IActivableComponent
{
    bool Disabled { get; }
}