using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void OnQuitButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
