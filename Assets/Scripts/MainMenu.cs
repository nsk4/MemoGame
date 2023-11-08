using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
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
        Settings.PlayerName = value;
    }

    public void OnBackSettingsButtonClicked()
    {
        Settings.StoreToFile();
    }
}
