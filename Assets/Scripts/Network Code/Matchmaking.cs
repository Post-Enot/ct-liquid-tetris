using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public sealed class Matchmaking : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Scenes _scenes;
        [SerializeField] private PlayerProgress _playerProgress;

        private void Awake()
        {
            if (!PhotonNetwork.IsConnected)
            {
                InitNickName(_playerProgress.Nickname.Value);
                InitGameVersion();
                _ = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.AutomaticallySyncScene = true;
            }
            _playerProgress.Nickname.ValueChanged += InitNickName;
        }

        private void OnDestroy()
        {
            _playerProgress.Nickname.ValueChanged -= InitNickName;
        }

        public void StartMatchSearching()
        {
            _ = StartCoroutine(StartMatchmaking());
        }

        public void StopMatchSearching()
        {
            if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                _ = PhotonNetwork.LeaveRoom();
            }
        }

        public void InitNickName(string nickname)
        {
            PhotonNetwork.NickName = nickname;
        }

        public override void OnJoinedRoom()
        {
            CheckPlayersCount();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            CheckPlayersCount();
        }

        private void InitGameVersion()
        {
            PhotonNetwork.GameVersion = "1";
        }

        private IEnumerator StartMatchmaking()
        {
            if (PhotonNetwork.CurrentRoom != null)
            {
                _ = PhotonNetwork.LeaveRoom();
                yield return WaitUntilLeaveRoom();
            }
            if (!PhotonNetwork.IsConnectedAndReady)
            {
                yield return WaitUntilConnectedAndReady();
            }
            _ = PhotonNetwork.JoinRandomOrCreateRoom();
        }

        private IEnumerator WaitUntilConnectedAndReady()
        {
            do
            {
                yield return null;
            }
            while (!PhotonNetwork.IsConnectedAndReady);
        }

        private IEnumerator WaitUntilLeaveRoom()
        {
            do
            {
                yield return null;
            }
            while (PhotonNetwork.CurrentRoom != null);
        }

        private void CheckPlayersCount()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.LoadLevel(_scenes.OnlineMatchSceneName);
            }
        }
    }
}
