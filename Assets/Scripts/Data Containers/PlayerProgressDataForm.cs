using System;

namespace LiquidTetris.DataContainers
{
	[Serializable]
	public sealed class PlayerProgressDataForm
	{
		public int CoinsCount;
		public int DefeatsCount;
		public int WinsCount;
		public int BombsCount;
        public string Nickname;

		public PlayerProgressDataForm() { }

		public PlayerProgressDataForm(PlayerProgress playerProgress)
		{
			CoinsCount = (int)playerProgress.CoinsNumber.Value;
			DefeatsCount = (int)playerProgress.DefeatsNumber.Value;
			WinsCount = (int)playerProgress.WinsNumber.Value;
			BombsCount = (int)playerProgress.BombsNumber.Value;
            Nickname = (string)playerProgress.Nickname.Value;
		}
	}
}
