using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class NetworkPlayers : MonoBehaviourPunCallbacks
    {
        [SerializeField] private string _playerPrefabLocalPath;

        [Header("References for user:")]
        [SerializeField] private EntitySender _entitySender;
        [SerializeField] private FieldOverflowDetector _fieldOverflowDetector;

        public NetworkPlayer User { get; private set; }
        public NetworkPlayer Opponent { get; private set; }
        public NetworkPlayerDataCast OpponentDataCast { get; private set; }

        public event Action AllPlayersInited;
        public event Action UserGaveUp;
        public event Action OpponentGaveUp;
        public event Action OpponentDisconected;

        private void Start()
        {
            GameObject gameObject = PhotonNetwork.Instantiate(_playerPrefabLocalPath, default, default);
            var user = gameObject.GetComponent<NetworkPlayer>();
            user.Init(_entitySender, _fieldOverflowDetector);
        }

        public override void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += OnNetworkPlayerInited;
        }

        public override void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= OnNetworkPlayerInited;
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (Opponent == null)
            {
                OpponentDisconected?.Invoke();
            }
        }

        public void GaveUp()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                UserGaveUp?.Invoke();
            }
            else
            {
                photonView.RPC(nameof(RpcGaveUp), RpcTarget.MasterClient);
            }
        }

        [PunRPC] private void RpcGaveUp() => OpponentGaveUp?.Invoke();

        private void OnNetworkPlayerInited(EventData photonEvent)
        {
            if (photonEvent.Code == (byte)PhotonCustomEventCode.NetworkPlayerInited)
            {
                PhotonView photonView = PhotonNetwork.GetPhotonView((int)photonEvent.CustomData);
                var networkPlayer = photonView.GetComponent<NetworkPlayer>();
                if (photonView.IsMine)
                {
                    User = networkPlayer;
                }
                else
                {
                    Opponent = networkPlayer;
                    OpponentDataCast = new(networkPlayer);
                }
                if (User != null && Opponent != null)
                {
                    AllPlayersInited?.Invoke();
                }
            }
        }
    }
}
