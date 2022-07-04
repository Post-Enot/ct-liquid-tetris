using Photon.Pun;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    [RequireComponent(typeof(MatchTimer))]
    public class MatchTimerNetworkSynch : MonoBehaviourPun, IPunObservable
    {
        private MatchTimer _matchTimer;

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_matchTimer.TimeInSeconds.Value);
            }
            else
            {
                _matchTimer.TimeInSeconds.Value = (float)stream.ReceiveNext();
            }
        }

        private void Awake()
        {
            _matchTimer = GetComponent<MatchTimer>();
        }
    }
}
