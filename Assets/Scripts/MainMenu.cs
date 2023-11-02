using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private int pairCount;
    [SerializeField] private int minPairs;
    [SerializeField] private int maxPairs;
    [SerializeField] private TextMeshProUGUI pairsCountSettingsText;
    [SerializeField] private Slider pairsCountSettingsSlider;

    [SerializeField] private int effectCount;
    [SerializeField] private int minEffect;
    [SerializeField] private int maxEffect;
    [SerializeField] private TextMeshProUGUI effectCountSettingsText;
    [SerializeField] private Slider effectCountSettingsSlider;

    [SerializeField] private int minVolume;
    [SerializeField] private int maxVolume;
    [SerializeField] private TextMeshProUGUI volumeSettingsText;
    [SerializeField] private Slider volumeSettingsSlider;


    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuTheme");

        Settings.PairCount = pairCount;
        Settings.EffectPeriod = effectCount;
        pairsCountSettingsText.SetText(Settings.PairCount.ToString());
        pairsCountSettingsSlider.value = (Settings.PairCount - minPairs) / (float)(maxPairs - minPairs);
        effectCountSettingsText.SetText(Settings.EffectPeriod.ToString());
        effectCountSettingsSlider.value = (Settings.EffectPeriod - minEffect) / (float)(maxEffect - minEffect);
        volumeSettingsText.SetText(maxVolume.ToString());
        volumeSettingsSlider.value = 1;
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
            effectCountSettingsText.SetText(Settings.EffectPeriod.ToString());
        }
        else
        {
            effectCountSettingsText.SetText("Off");
        }
    }

    public void OnEffectCountSettingsValueChange(float value)
    {
        Settings.EffectPeriod = Mathf.RoundToInt((maxEffect - minEffect) * value) + minEffect;
        if (Settings.EffectPeriod < Settings.PairCount)
        {
            effectCountSettingsText.SetText(Settings.EffectPeriod.ToString());
        }
        else
        {
            effectCountSettingsText.SetText("Off");
        }
    }

    public void OnVolumeSettingsValueChange(float value)
    {
        AudioListener.volume = value;
        volumeSettingsText.SetText((Mathf.RoundToInt((maxVolume - minVolume) * value) + minVolume).ToString());
    }
}
