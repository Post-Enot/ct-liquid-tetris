using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace LiquidTetris.UI.Input
{
    public sealed class UITouchDetectionField : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Space]
        [SerializeField] private bool _framePosition;

        [Header("Events:")]
        [SerializeField] private UnityEvent<Vector2> _onPointerDown;
        [SerializeField] private UnityEvent<Vector2> _onPointerMove;
        [SerializeField] private UnityEvent<Vector2> _onPointerUp;

        public Vector2 TouchPositionInWorldSpace { get; private set; }
        public UnityEvent<Vector2> OnPointerDown => _onPointerDown;
        public UnityEvent<Vector2> OnPointerMove => _onPointerMove;
        public UnityEvent<Vector2> OnPointerUp => _onPointerUp;

        private RectTransform _rectTransform;
        private Vector2 _bottomLeftCorner;
        private Vector2 _topRightCorner;

        public void OnBeginDrag(PointerEventData eventData)
        {
            UpdateTouchPosition(eventData.pointerCurrentRaycast.worldPosition);
            _onPointerDown?.Invoke(TouchPositionInWorldSpace);
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateTouchPosition(eventData.pointerCurrentRaycast.worldPosition);
            _onPointerMove?.Invoke(TouchPositionInWorldSpace);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            UpdateTouchPosition(eventData.pointerCurrentRaycast.worldPosition);
            _onPointerUp?.Invoke(TouchPositionInWorldSpace);
        }

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            Vector3[] corners = new Vector3[4];
            _rectTransform.GetWorldCorners(corners);
            _bottomLeftCorner = corners[0];
            _topRightCorner = corners[2];
        }

        private Vector2 FrameTouchPosition(Vector2 touchPosition)
        {
            if (touchPosition.x > _topRightCorner.x)
            {
                touchPosition.x = _topRightCorner.x;
            }
            else if (touchPosition.x < _bottomLeftCorner.x)
            {
                touchPosition.x = _bottomLeftCorner.x;
            }
            if (touchPosition.y > _topRightCorner.y)
            {
                touchPosition.y = _topRightCorner.y;
            }
            else if (touchPosition.y < _bottomLeftCorner.y)
            {
                touchPosition.y = _bottomLeftCorner.y;
            }
            return touchPosition;
        }

        private void UpdateTouchPosition(Vector2 position)
        {
            if (_framePosition)
            {
                position = FrameTouchPosition(position);
            }
            TouchPositionInWorldSpace = position;
        }
    }
}
