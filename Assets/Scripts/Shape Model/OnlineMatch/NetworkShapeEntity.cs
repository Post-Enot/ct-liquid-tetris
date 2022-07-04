using UnityEngine;
using Photon.Pun;

namespace LiquidTetris.NetworkCode
{
    public class NetworkShapeEntity : MonoBehaviourPun, IShapeEntity
    {
        public Transform Transform => transform;
        public IShapeModel Model { get; private set; }
        public IShapeDrawModule DrawModule { get; private set; }
        public IShapeBehaviourModule BehaviourModule { get; private set; }
        public IShapeVisualEffectsModule VisualEffectsModule { get; private set; }
        public ISpawnable Spawnable { get; private set; }

        private void Awake()
        {
            Model = GetComponent<NetworkShapeModel>();
            DrawModule = GetComponent<IShapeDrawModule>();
            BehaviourModule = GetComponent<IShapeBehaviourModule>();
            VisualEffectsModule = GetComponent<IShapeVisualEffectsModule>();
            Spawnable = GetComponent<ISpawnable>();
        }
    }
}
