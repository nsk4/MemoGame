using System;

public class Score
{
    public string PlayerName { get; set; }
    public TimeSpan GameTime { get; set; }
    public int PairCount { get; set; }
    public int EffectPeriod { get; set; }
    public int FlipCount { get; set; }

    public Score(string playerName, TimeSpan gameTime, int pairCount, int effectPeriod, int flipCount)
    {
        PlayerName = playerName;
        GameTime = gameTime;
        PairCount = pairCount;
        EffectPeriod = effectPeriod;
        FlipCount = flipCount;
    }

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

    override public string ToString()
    {
        return string.Format("{0}||{1}||{2}||{3}||{4}", PlayerName, GameTime.ToString(), PairCount, EffectPeriod, FlipCount);
    }

    public static Score FromString(string str)
    {
        string[] splits = str.Split("||");
        return new Score(splits[0], TimeSpan.Parse(splits[1]), int.Parse(splits[2]), int.Parse(splits[3]), int.Parse(splits[4]));
    }
}
