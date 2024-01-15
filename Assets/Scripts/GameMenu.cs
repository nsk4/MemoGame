using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handle game menu logic.
/// </summary>
public class GameMenu : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TextMeshProUGUI gameOverMenuTime;
    [SerializeField] private TextMeshProUGUI gameTimeText;
    [SerializeField] private TextMeshProUGUI gameScoreText;

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

    /// <summary>
    /// Event triggered on game over.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event parameters</param>
    public void OnGameOverEvent(object sender, EventArgs e)
    {
        gameTime.Stop();
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.Stop("GameTheme");
        audioManager.Play("VictoryAlert");
        audioManager.PlayDelayed("VictoryTheme", 3);
        gameOverMenu.SetActive(true);
        gameOverMenuTime.SetText(gameTime.Elapsed.ToString("hh\\:mm\\:ss"));
        Score s = new(Settings.PlayerName, gameTime.Elapsed, Settings.PairCount, Settings.EffectPeriod, boardManager.GetTotalFlipCount());
        gameScoreText.SetText(s.CalculateScore().ToString());
        new ScoreTracker().AddScore(s);
    }

    /// <summary>
    /// Stop game and return back to main menu.
    /// </summary>
    public void OnQuitButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Pause game.
    /// </summary>
    public void PauseGame()
    {
        gameTime.Stop();
    }

    /// <summary>
    /// Resume game.
    /// </summary>
    public void ResumeGame()
    {
        gameTime.Start();
    }
}
