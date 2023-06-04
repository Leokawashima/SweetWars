public static class Name
{
    public enum Scene { Title, Load, Game, }
    public static int SceneName_Int(Scene scene) { return (int)scene; }
}