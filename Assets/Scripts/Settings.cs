using UnityEngine;
/// <summary>
/// Static class that contains game settings.
/// </summary>
public static class Settings
{
    public static string PlayerName = "";
    public static int PairCount = 1;
    public static int EffectPeriod = 1;
    public static float Volume = 1.0f;

    /// <summary>
    /// Load settings from PlayerPrefs.
    /// </summary>
    public static void LoadFromFile()
    {
        PlayerName = PlayerPrefs.GetString("PlayerName", "SuperMemory");
        PairCount = PlayerPrefs.GetInt("PairCount", 1);
        EffectPeriod = PlayerPrefs.GetInt("EffectPeriod", 1);
        Volume = PlayerPrefs.GetFloat("Volume", 1.0f);
    }

    /// <summary>
    /// Store settings to PlayerPrefs.
    /// </summary>
    public static void StoreToFile()
    {
        PlayerPrefs.SetString("PlayerName", PlayerName);
        PlayerPrefs.SetInt("PairCount", PairCount);
        PlayerPrefs.SetInt("EffectPeriod", EffectPeriod);
        PlayerPrefs.SetFloat("Volume", Volume);
        PlayerPrefs.Save();
    }
}
