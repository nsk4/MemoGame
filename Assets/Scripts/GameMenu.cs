using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameObject gameOverMenu;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("GameTheme");
        boardManager.GameOverEvent += OnGameOverEvent;
    }

    public void OnGameOverEvent(object sender, EventArgs e)
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.Stop("GameTheme");
        audioManager.Play("VictoryAlert");
        audioManager.PlayDelayed("VictoryTheme", 3);
        gameOverMenu.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
