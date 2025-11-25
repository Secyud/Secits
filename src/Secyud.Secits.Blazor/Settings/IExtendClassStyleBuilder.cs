namespace Secyud.Secits.Blazor.Settings;

public interface IExtendClassStyleBuilder : IIsPlugin
{
    void BuildExtendClassStyle(ClassStyleContext context);
}