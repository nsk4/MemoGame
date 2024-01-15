using System.Collections.Generic;
using System.IO;

/// <summary>
/// Score tracker for managing scores.
/// </summary>
public class ScoreTracker
{
    private static readonly string scoreFilePath = "Assets/Resources/scores.txt";

    private List<Score> scoreList;

    /// <summary>
    /// Construct a new score tracher.
    /// </summary>
    public ScoreTracker()
    {
        scoreList = new List<Score>();

        StreamReader reader = new StreamReader(scoreFilePath);
        while (!reader.EndOfStream)
        {
            scoreList.Add(Score.FromString(reader.ReadLine()));
        }
        reader.Close();
    }

    /// <summary>
    /// Add score to the list of scores and save it to a file.
    /// </summary>
    /// <param name="s"></param>
    public void AddScore(Score s)
    {
        scoreList.Add(s);
        StreamWriter writer = new StreamWriter(scoreFilePath, true);
        writer.WriteLine(s.ToString());
        writer.Close();
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
