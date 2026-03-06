namespace Secyud.Secits.Blazor.Plugins;

/// <summary>
/// 在输入时触发，用于设置延迟输入，字符串解析等功能
/// </summary>
public interface ISpInputHandler : ISPlugin
{
    Task HandleInputAsync(string? str);
}