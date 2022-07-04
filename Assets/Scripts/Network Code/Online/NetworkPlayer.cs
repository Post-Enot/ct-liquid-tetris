using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class NetworkPlayer : MonoBehaviourPun
    {
        [Header("Component references:")]
        [SerializeField] private GameRules _gameRules;
        [SerializeField] private PlayerProgress _playerProgress;

        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                ScoreUpdated?.Invoke(_score);
            }
        }
        public int ContributionInCoins { get; private set; }
        public bool IsShapeSendingAllowed { get; private set; }

        public event Action<int> ScoreUpdated;
        public event Action<NetworkShapeEntity> ShapeArrived;
        public event Action ShapeSendingAllowed;
        public event Action ShapeSendingProhibited;

        private int _score;
        private EntitySender _entitySender;
        private FieldOverflowDetector _fieldOverflowDetector;

        public void Init(EntitySender entitySender, FieldOverflowDetector fieldOverflowDetector)
        {
            if (photonView.IsMine)
            {
                _entitySender = entitySender;
                _fieldOverflowDetector = fieldOverflowDetector;
                int contributionInCoins = _gameRules.CalculateContribution(_playerProgress.CoinsNumber.Value);
                if (photonView.IsMine)
                {
                    InitSendingStatus();
                    photonView.RPC(nameof(RpcInitFields), RpcTarget.All, contributionInCoins, _entitySender.IsShapeSendingAllowed);
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void SendShape(NetworkShapeEntity shapeEntity)
        {
            photonView.RPC(nameof(RpcSendShape), RpcTarget.All, shapeEntity.photonView.ViewID);
        }

        private void RpcSendShape(int shapeEntityPhotonViewID)
        {
            PhotonView photonView = PhotonNetwork.GetPhotonView(shapeEntityPhotonViewID);
            NetworkShapeEntity shapeEntity = photonView.GetComponent<NetworkShapeEntity>();
            ShapeArrived?.Invoke(shapeEntity);
        }

        private void InitSendingStatus()
        {
            _entitySender.ShapeSendingAllowed += AllowShapeSending;
            _entitySender.ShapeSendingProhibited += ProhibitShapeSending;
        }

        private void AllowShapeSending() => photonView.RPC(nameof(RpcAllowShapeSending), RpcTarget.All);

        private void ProhibitShapeSending() => photonView.RPC(nameof(RpcProhibitShapeSending), RpcTarget.All);

        [PunRPC]
        private void RpcAllowShapeSending()
        {
            IsShapeSendingAllowed = true;
            ShapeSendingAllowed?.Invoke();
        }

        [PunRPC]
        private void RpcProhibitShapeSending()
        {
            IsShapeSendingAllowed = false;
            ShapeSendingProhibited?.Invoke();
        }

        [PunRPC]
        private void RpcInitFields(int contributionInCoins, bool isShapeSendingAllowed)
        {
            RaiseEventOptions raiseEventOptions = new() { Receivers = ReceiverGroup.All };
            ContributionInCoins = contributionInCoins;
            IsShapeSendingAllowed = isShapeSendingAllowed;
            if (!photonView.IsMine)
            {
                _ = PhotonNetwork.RaiseEvent(
                    (byte)PhotonCustomEventCode.NetworkPlayerInited,
                    photonView.ViewID,
                    raiseEventOptions,
                    SendOptions.SendReliable);
            }
        }
    }
}
