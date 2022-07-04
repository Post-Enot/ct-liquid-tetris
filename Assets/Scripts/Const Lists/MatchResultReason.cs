namespace LiquidTetris
{
    public enum MatchResultReason : byte
    {
        None = 0,
        OverflowField = 1,
        ScoreAdvantage = 2,
        Surrendered = 3
    }
}
