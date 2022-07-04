namespace LiquidTetris.NetworkCode
{
    public class MatchResultForm
    {
        public MatchResultForm(
            MatchResult matchResult,
            MatchResultReason matchResultReason,
            int coinsDifference,
            int userScore,
            int opponentScore,
            string userNickname,
            string opponentNickname)
        {
            MatchResult = matchResult;
            MatchResultReason = matchResultReason;
            CoinsDifference = coinsDifference;
            UserScore = userScore;
            OpponentScore = opponentScore;
            UserNickname = userNickname;
            OpponentNickname = opponentNickname;
        }

        public readonly MatchResult MatchResult;
        public readonly MatchResultReason MatchResultReason;
        public readonly int CoinsDifference;
        public readonly int UserScore;
        public readonly int OpponentScore;
        public readonly string UserNickname;
        public readonly string OpponentNickname;
    }
}
