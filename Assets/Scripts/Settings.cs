public static class Settings
{
    private static readonly string settingsFilePath = "Assets/Resources/settings.ini";

    public static string PlayerName = "";
    public static int PairCount = 1;
    public static int EffectPeriod = 1;
    public static float Volume = 1.0f;

    public static void LoadFromFile()
    {
        INIParser iniParser = new INIParser();
        iniParser.Open(settingsFilePath);
        PlayerName = iniParser.ReadValue("Settings", "PlayerName", "");
        PairCount = iniParser.ReadValue("Settings", "PairCount", 1);
        EffectPeriod = iniParser.ReadValue("Settings", "EffectPeriod", 1);
        Volume = (float)iniParser.ReadValue("Settings", "Volume", 1.0);
        iniParser.Close();
    }

    public static void StoreToFile()
    {
        INIParser iniParser = new INIParser();
        iniParser.Open(settingsFilePath);
        iniParser.WriteValue("Settings", "PlayerName", PlayerName);
        iniParser.WriteValue("Settings", "PairCount", PairCount);
        iniParser.WriteValue("Settings", "EffectPeriod", EffectPeriod);
        iniParser.WriteValue("Settings", "Volume", Volume);
        iniParser.Close();
    }
}
