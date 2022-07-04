using Photon.Pun;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class NetworkShapeDrawingAction : MonoBehaviour
    {
        [Header("Component references:")]
        [SerializeField] private ShapeDrawer _shapeDrawer;
        [SerializeField] private NetworkShapeFabric _shapeFabric;
        [SerializeField] private EntitySender _entitySender;

        public bool IsEnabled { get; private set; }

        private NetworkShapeEntity _drawingShapeEntity;

        public void EnableDrawing()
        {
            IsEnabled = true;
        }

        public void DisableDrawing()
        {
            IsEnabled = false;
        }

        public void HandleTouchDown(Vector2 touchPositionInWorldSpace)
        {
            if (IsEnabled && _drawingShapeEntity == null)
            {
                _drawingShapeEntity = _shapeFabric.InstantiateShape(_shapeDrawer.DrawingFieldCenter);
                var shapeModel = _drawingShapeEntity.Model as NetworkShapeModel;
                shapeModel.photonView.RPC(nameof(shapeModel.DisableGameObject), RpcTarget.Others);
                _shapeDrawer.HandleTouchDown(touchPositionInWorldSpace, _drawingShapeEntity);
            }
        }

        public void HandleTouchMove(Vector2 touchPositionInWorldSpace)
        {
            if (_drawingShapeEntity != null)
            {
                if (IsEnabled)
                {
                    _shapeDrawer.HandleTouchMove(touchPositionInWorldSpace, _drawingShapeEntity);
                }
                else
                {
                    PhotonNetwork.Destroy(_drawingShapeEntity.GetComponent<PhotonView>());
                }
            }
        }

        public void HandleTouchUp()
        {
            if (_drawingShapeEntity != null)
            {
                if (IsEnabled)
                {
                    _entitySender.SendShape(_drawingShapeEntity);
                    _drawingShapeEntity = null;
                }
                else
                {
                    PhotonNetwork.Destroy(_drawingShapeEntity.GetComponent<PhotonView>());
                }
            }
        }
    }
}
