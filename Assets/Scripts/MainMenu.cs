using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnBackToMenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
