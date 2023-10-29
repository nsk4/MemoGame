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

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuTheme");
        PlayerPrefs.SetInt(SettingsConstants.PairCount, pairCount);
        PlayerPrefs.SetInt(SettingsConstants.EffectPeriod, effectCount);

        pairsCountSettingsText.SetText(pairCount.ToString());
        pairsCountSettingsSlider.value = (pairCount - minPairs) / (float)(maxPairs - minPairs);
        effectCountSettingsText.SetText(effectCount.ToString());
        effectCountSettingsSlider.value = (effectCount - minEffect) / (float)(maxEffect - minEffect);
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
        pairCount = Mathf.RoundToInt((maxPairs - minPairs) * value) + minPairs;
        pairsCountSettingsText.SetText(pairCount.ToString());
        PlayerPrefs.SetInt(SettingsConstants.PairCount.ToString(), pairCount);
        if (effectCount < pairCount)
        {
            effectCountSettingsText.SetText(effectCount.ToString());
        }
        else
        {
            effectCountSettingsText.SetText("Off");
        }
    }

    public void OnEffectCountSettingsValueChange(float value)
    {
        effectCount = Mathf.RoundToInt((maxEffect - minEffect) * value) + minEffect;
        PlayerPrefs.SetInt(SettingsConstants.EffectPeriod.ToString(), effectCount);
        if (effectCount < pairCount)
        {
            effectCountSettingsText.SetText(effectCount.ToString());
        }
        else
        {
            effectCountSettingsText.SetText("Off");
        }
    }
}
