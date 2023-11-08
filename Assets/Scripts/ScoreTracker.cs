using System.Collections.Generic;
using System.IO;

public class ScoreTracker
{
    private static readonly string scoreFilePath = "Assets/Resources/scores.txt";

    private List<Score> scoreList;

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

    public void AddScore(Score s)
    {
        scoreList.Add(s);
        StreamWriter writer = new StreamWriter(scoreFilePath, true);
        writer.WriteLine(s.ToString());
        writer.Close();
    }
}
