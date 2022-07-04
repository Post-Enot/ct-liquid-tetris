using System;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class MatchProgressManipulation : MonoBehaviour
    {
        [SerializeField] private GameRules _gameRules;
        [SerializeField] private PlayerProgress _playerProgress;
        [SerializeField] private NetworkPlayers _players;

        public int Bank { get; private set; }
        public int UserContribution { get; private set; }
        public int OpponentContribution { get; private set; }

        private int _initialCoinsNumberValue;

        public void OnMatchStart()
        {
            _initialCoinsNumberValue = _playerProgress.CoinsNumber.Value;
            UserContribution = _gameRules.CalculateContribution(_playerProgress.CoinsNumber.Value);
            OpponentContribution = _gameRules.CalculateContribution(_players.OpponentDataCast.Coins);
            Bank = UserContribution + OpponentContribution + _gameRules.PrizeInCoinsForWin;
            _playerProgress.CoinsNumber.Value -= UserContribution;
            _playerProgress.DefeatsNumber.Value += 1;
        }

        public int GetCoinsDifferenceByMatchResult(MatchResult matchResult)
        {
            return matchResult switch
            {
                MatchResult.Win => _gameRules.PrizeInCoinsForWin + OpponentContribution,
                MatchResult.Def => UserContribution,
                MatchResult.Draw => 0,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void OnMatchEnd(MatchResultForm matchResultForm)
        {
            if (matchResultForm.MatchResult == MatchResult.Win)
            {
                _playerProgress.CoinsNumber.Value += Bank;
                _playerProgress.DefeatsNumber.Value -= 1;
                _playerProgress.WinsNumber.Value += 1;
            }
            else if (matchResultForm.MatchResult == MatchResult.Draw)
            {
                _playerProgress.CoinsNumber.Value = _initialCoinsNumberValue;
            }
        }
    }
}
