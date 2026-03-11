namespace Secyud.Secits.Blazor;

public static partial class SJsModules
{
    public const string Name = "secits";

    public static class Theme
    {
        public const string ModuleName = Name + ".themeManager";
        public const string SetCurrentStyle = ModuleName + ".setCurrentStyle";
        public const string ReplaceStyles = ModuleName + ".replaceStyles";
    }

    public static class Element
    {
        public const string ModuleName = Name + ".elementManager";
        public const string Invoke = ModuleName + ".invoke";
        public const string InvokeVoid = ModuleName + ".invokeVoid";
    }

    public static class Event
    {
        public const string ModuleName = Name + ".eventManager";
        public const string GetId = ModuleName + ".getId";
        public const string Create = ModuleName + ".create";
        public const string Delete = ModuleName + ".delete";
    }
}