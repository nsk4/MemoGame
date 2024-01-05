using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private const int MaxPlayerNameLength = 15;
    [SerializeField] private TMPro.TMP_InputField playerNameSettingsText;

    [SerializeField] private int minPairs;
    [SerializeField] private int maxPairs;
    [SerializeField] private TextMeshProUGUI pairsCountSettingsText;
    [SerializeField] private Slider pairsCountSettingsSlider;

    [SerializeField] private int minEffect;
    [SerializeField] private int maxEffect;
    [SerializeField] private TextMeshProUGUI effectPeriodSettingsText;
    [SerializeField] private Slider effectPeriodSettingsSlider;

    [SerializeField] private int minVolume;
    [SerializeField] private int maxVolume;
    [SerializeField] private TextMeshProUGUI volumeSettingsText;
    [SerializeField] private Slider volumeSettingsSlider;

    [SerializeField] private TextMeshProUGUI scoreboardTableText;


    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuTheme");

        // Load settings from ini file and initialize settings menu.
        Settings.LoadFromFile();
        playerNameSettingsText.text = Settings.PlayerName;
        pairsCountSettingsText.SetText(Settings.PairCount.ToString());
        pairsCountSettingsSlider.value = (Settings.PairCount - minPairs) / (float)(maxPairs - minPairs);
        effectPeriodSettingsText.SetText(Settings.EffectPeriod.ToString());
        effectPeriodSettingsSlider.value = (Settings.EffectPeriod - minEffect) / (float)(maxEffect - minEffect);
        volumeSettingsText.SetText((Mathf.RoundToInt((maxVolume - minVolume) * Settings.Volume) + minVolume).ToString());
        volumeSettingsSlider.value = Settings.Volume;
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Quitting the game");
        Application.Quit();
    }

    public void OnPairCountSettingsValueChange(float value)
    {
        Settings.PairCount = Mathf.RoundToInt((maxPairs - minPairs) * value) + minPairs;
        pairsCountSettingsText.SetText(Settings.PairCount.ToString());
        if (Settings.EffectPeriod < Settings.PairCount)
        {
            effectPeriodSettingsText.SetText(Settings.EffectPeriod.ToString());
        }
        else
        {
            effectPeriodSettingsText.SetText("Off");
        }
    }

    public void OnEffectPeriodSettingsValueChange(float value)
    {
        Settings.EffectPeriod = Mathf.RoundToInt((maxEffect - minEffect) * value) + minEffect;
        if (Settings.EffectPeriod < Settings.PairCount)
        {
            effectPeriodSettingsText.SetText(Settings.EffectPeriod.ToString());
        }
        else
        {
            effectPeriodSettingsText.SetText("Off");
        }
    }

    public void OnVolumeSettingsValueChange(float value)
    {
        Settings.Volume = value;
        AudioListener.volume = value;
        volumeSettingsText.SetText((Mathf.RoundToInt((maxVolume - minVolume) * value) + minVolume).ToString());
    }

    public void OnPlayerNameSettingsValueEndEdit(string value)
    {
        if (value.Length > MaxPlayerNameLength)
        {
            value = value.Substring(0, MaxPlayerNameLength);
            playerNameSettingsText.text = value;
        }
        Settings.PlayerName = value;
    }

    public void OnBackSettingsButtonClicked()
    {
        Settings.StoreToFile();
    }

    public void OnOpenScoreboardButtonClicked()
    {
        List<Score> scores = new ScoreTracker().GetScoreList().OrderByDescending(x => x.CalculateScore()).Take(10).ToList();
        string scoreboardTable = "<mspace=15.0 em>"; // Force monospace font
        scoreboardTable += string.Format("{0,15} | {1,6} | {2,8} | {3,5} | {4,5} | {5,6}{6}",
            "Name", "Score", "Time", "Flips", "Pairs", "Effect", System.Environment.NewLine);
        foreach (Score score in scores)
        {
            scoreboardTable += string.Format("{0,15} | {1,6} | {2,8} | {3,5} | {4,5} | {5,6}{6}",
                score.PlayerName.Substring(0, System.Math.Min(score.PlayerName.Length, MaxPlayerNameLength)),
                System.Math.Round(score.CalculateScore(), 2),
                score.GameTime.ToString("hh\\:mm\\:ss"),
                score.FlipCount,
                score.PairCount,
                score.EffectPeriod,
                System.Environment.NewLine);
        }

        scoreboardTableText.SetText(scoreboardTable);
    }
}
