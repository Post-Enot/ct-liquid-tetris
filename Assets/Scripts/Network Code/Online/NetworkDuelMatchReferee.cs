using FieldIndicators;
using LiquidTetris.UI.Game;
using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace LiquidTetris.NetworkCode
{
    public class NetworkDuelMatchReferee : MonoBehaviourPun
    {
        [Header("Reference fields:")]
        [SerializeField] private IntReferenceField _userScoreField;
        [SerializeField] private IntReferenceField _opponentScoreField;

        [Header("Component references:")]
        [SerializeField] private GameRules _gameRules;
        [SerializeField] private NetworkPlayers _players;
        [SerializeField] private MatchTimer _matchTimer;
        [SerializeField] private GameField _userGameField;
        [SerializeField] private GameField _opponentGameField;
        [SerializeField] private PopupFabric _userPopupFabric;
        [SerializeField] private PopupFabric _opponentPopupFabric;
        [SerializeField] private MatchProgressManipulation _matchProgressManipulation;

        [Header("Events:")]
        [SerializeField] private UnityEvent<MatchResultForm> _matchEnd = new();

        public EntityController ShapeControl { get; private set; }
        public ShapeSpecificator ShapeSpecificator { get; private set; }
        public NetworkShapeFabric ShapeFabric { get; private set; }
        public bool IsDrawingAllowed => _players.Opponent.IsShapeSendingAllowed;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                _players.AllPlayersInited += StartMatch;
            }
            ShapeFabric = GetComponent<NetworkShapeFabric>();
            ShapeControl = GetComponent<EntityController>();
            ShapeSpecificator = GetComponent<ShapeSpecificator>();
        }

        public void SendShape(NetworkShapeEntity shapeEntity)
        {
            var shapeModel = shapeEntity.Model as NetworkShapeModel;
            shapeModel.photonView.RPC(nameof(shapeModel.EnableGameObject), RpcTarget.Others);
            shapeModel.photonView.TransferOwnership(_players.Opponent.photonView.Owner);
            _players.Opponent.SendShape(shapeEntity);
        }

        public void EndMatch(MatchResult result, MatchResultReason reason)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                MatchResult opponentResult = result switch
                {
                    MatchResult.Win => MatchResult.Def,
                    MatchResult.Def => MatchResult.Win,
                    MatchResult.Draw => MatchResult.Draw,
                    _ => throw new ArgumentOutOfRangeException()
                };
                SendMatchResult(result, reason);
                photonView.RPC(nameof(SendMatchResult), RpcTarget.Others, opponentResult, reason);
            }
        }

        private void StartMatch()
        {
            _players.AllPlayersInited -= StartMatch;
            if (PhotonNetwork.IsMasterClient)
            {
                _matchTimer.TimeInSeconds.Value = _gameRules.MatchDurationInSeconds;
                _matchTimer.StartCountdown();
                _matchTimer.TimeLeft += EndMatchAtTheEndOfTheTime;
            }
        }

        private void EndMatchAtTheEndOfTheTime()
        {
            MatchResult result;
            if (_players.User.Score > _players.Opponent.Score)
            {
                result = MatchResult.Win;
            }
            else if (_players.User.Score < _players.Opponent.Score)
            {
                result = MatchResult.Def;
            }
            else
            {
                result = MatchResult.Draw;
            }
            EndMatch(result, MatchResultReason.ScoreAdvantage);
        }

        [PunRPC]
        private void SendMatchResult(MatchResult result, MatchResultReason reason)
        {
            var form = new MatchResultForm(
                result,
                reason,
                _matchProgressManipulation.GetCoinsDifferenceByMatchResult(result),
                _players.User.Score,
                _players.OpponentDataCast.Score,
                PhotonNetwork.NickName,
                _players.OpponentDataCast.Nickname);
            _matchEnd?.Invoke(form);
        }
    }
}
