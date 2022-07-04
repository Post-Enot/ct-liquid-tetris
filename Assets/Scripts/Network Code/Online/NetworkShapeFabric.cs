using Photon.Pun;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class NetworkShapeFabric : MonoBehaviour
    {
        [SerializeField] private string _networkShapePrefabLocalPath;

        public NetworkShapeEntity InstantiateShape(Vector2 worldPosition)
        {
            GameObject networkShapeObject = PhotonNetwork.Instantiate(_networkShapePrefabLocalPath, worldPosition, Quaternion.identity);
            return networkShapeObject.GetComponent<NetworkShapeEntity>();
        }
    }
}
