using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private int minPairs;
    [SerializeField] private int maxPairs;
    [SerializeField] private TextMeshProUGUI pairsCountSettingsText;

    [SerializeField] private int minEffect;
    [SerializeField] private int maxEffect;
    [SerializeField] private TextMeshProUGUI effectCountSettingsText;

    private int pairCount;
    private int effectCount;

    private void Start()
    {
        pairCount = minPairs;
        effectCount = minEffect;
        pairsCountSettingsText?.SetText(pairCount.ToString());
        effectCountSettingsText?.SetText(effectCount.ToString());
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
        pairsCountSettingsText?.SetText(pairCount.ToString());
        if (effectCount < pairCount)
        {
            effectCountSettingsText?.SetText(effectCount.ToString());
        }
        else
        {
            effectCountSettingsText?.SetText("Off");
        }
    }

    public void OnEffectCountSettingsValueChange(float value)
    {
        effectCount = Mathf.RoundToInt((maxEffect - minEffect) * value) + minEffect;
        if (effectCount < pairCount)
        {
            effectCountSettingsText?.SetText(effectCount.ToString());
        }
        else
        {
            effectCountSettingsText?.SetText("Off");
        }
    }
}
