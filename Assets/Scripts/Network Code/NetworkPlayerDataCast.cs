namespace LiquidTetris.NetworkCode
{
    public class NetworkPlayerDataCast
    {
        public NetworkPlayerDataCast(NetworkPlayer networkPlayer)
        {
            _networkPlayer = networkPlayer;
            _networkPlayer.ScoreUpdated += (int score) => Score = score;
            Nickname = _networkPlayer.photonView.Owner.NickName;
            Coins = networkPlayer.ContributionInCoins;
        }

        public int Score { get; private set; }
        public int Coins { get; private set; }
        public string Nickname { get; private set; }

        private readonly NetworkPlayer _networkPlayer;
    }
}
