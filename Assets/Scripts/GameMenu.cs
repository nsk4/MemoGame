using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TextMeshProUGUI gameOverMenuTime;
    [SerializeField] private TextMeshProUGUI gameTimeText;

    private Stopwatch gameTime;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("GameTheme");
        boardManager.GameOverEvent += OnGameOverEvent;

        gameTimeText.SetText("");

        gameTime = new Stopwatch();
        gameTime.Start();
    }

    private void Update()
    {
        gameTimeText.SetText(gameTime.Elapsed.ToString("hh\\:mm\\:ss"));
    }

    public void OnGameOverEvent(object sender, EventArgs e)
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.Stop("GameTheme");
        audioManager.Play("VictoryAlert");
        audioManager.PlayDelayed("VictoryTheme", 3);
        gameOverMenu.SetActive(true);
        gameOverMenuTime.SetText(gameTime.Elapsed.ToString("hh\\:mm\\:ss"));

        // TODO: add score to score list
    }

    public void OnQuitButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        gameTime.Stop();
    }

    public void ResumeGame()
    {
        gameTime.Start();
    }
}
