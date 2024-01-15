using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Score tracker for managing scores.
/// </summary>
public class ScoreTracker
{
    private List<Score> scoreList;

    /// <summary>
    /// Construct a new score tracher and read any existing scores from PlayerPrefs.
    /// </summary>
    public ScoreTracker()
    {
        scoreList = new List<Score>();


        string allScores = PlayerPrefs.GetString("Scores", "");
        foreach (string scoreText in allScores.Split('\n'))
        {
            if (scoreText?.Length == 0)
            {
                continue;
            }
            scoreList.Add(Score.FromString(scoreText));
        }
    }

    /// <summary>
    /// Add score to the list of scores and save it to PlayerPrefs.
    /// </summary>
    /// <param name="s"></param>
    public void AddScore(Score s)
    {
        scoreList.Add(s);

        string allScores = "";
        foreach (Score score in scoreList)
        {
            allScores += score.ToString() + "\n";
        }
        PlayerPrefs.SetString("Scores", allScores);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Get the score list.
    /// </summary>
    /// <returns>List of scores.</returns>
    public List<Score> GetScoreList()
    {
        return scoreList;
    }


}
