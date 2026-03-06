namespace Secyud.Secits.Blazor.Plugins;

/// <summary>
/// 用于值处理时触发，校验，同步等功能
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface ISpValueHandler<in TValue> : ISPlugin
{
    Task HandleValueAsync(TValue? value);
}