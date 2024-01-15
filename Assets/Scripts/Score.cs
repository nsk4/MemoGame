using System;

/// <summary>
/// Wrap the game score.
/// </summary>
public class Score
{
    public string PlayerName { get; set; }
    public TimeSpan GameTime { get; set; }
    public int PairCount { get; set; }
    public int EffectPeriod { get; set; }
    public int FlipCount { get; set; }

    /// <summary>
    /// Construct a new game score object.
    /// </summary>
    /// <param name="playerName">Player name</param>
    /// <param name="gameTime">Game time</param>
    /// <param name="pairCount">Number of tile pairs used for the game.</param>
    /// <param name="effectPeriod">Tile swap effect period.</param>
    /// <param name="flipCount">Total number of flips required to win the game.</param>
    public Score(string playerName, TimeSpan gameTime, int pairCount, int effectPeriod, int flipCount)
    {
        PlayerName = playerName;
        GameTime = gameTime;
        PairCount = pairCount;
        EffectPeriod = effectPeriod;
        FlipCount = flipCount;
    }

    /// <summary>
    /// Calculate score based on game parameters. More difficult settings yield a higher score.
    /// </summary>
    /// <returns>Calculated game score.</returns>
    public float CalculateScore()
    {
        // score = total pairs + pair-effect ratio + pair-flip ratio + pair time ratio 
        float score = PairCount;
        if (EffectPeriod < PairCount)
        {
            score += PairCount / MathF.Max(EffectPeriod, 1);
        }
        score += PairCount * PairCount / (FlipCount / 2.0f);
        score += PairCount * PairCount / (float)GameTime.TotalSeconds;
        return score;
    }

    /// <summary>
    /// Convert game score to a string.
    /// </summary>
    /// <returns>Game score formatted as a string.</returns>
    override public string ToString()
    {
        return string.Format("{0}||{1}||{2}||{3}||{4}", PlayerName, GameTime.ToString(), PairCount, EffectPeriod, FlipCount);
    }

    /// <summary>
    /// Convert string to a game score.
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <returns>Game score formatted from a string.</returns>
    public static Score FromString(string str)
    {
        string[] splits = str.Split("||");
        return new Score(splits[0], TimeSpan.Parse(splits[1]), int.Parse(splits[2]), int.Parse(splits[3]), int.Parse(splits[4]));
    }
}
