using CoroutineShells;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace LiquidTetris.NetworkCode
{
    public class EntityController : MonoBehaviour
    {
        [SerializeField] private GameRules _gameRules;
        [SerializeField] private float _maxShapeXVelocity;
        [SerializeField] private float _movingFactor;

        [Header("Events:")]
        [SerializeField] private UnityEvent _breakedControl;
        [SerializeField] private UnityEvent _takenControl;

        public bool IsControlEntity { get; private set; }

        public UnityEvent BreakedControl => _breakedControl;
        public UnityEvent TakenControl => _takenControl;

        private IShapeEntity _shapeEntity;
        private float _previousTouchWorldPositionX;
        private UniqueCoroutine _countDownToBreakControl;

        private void Awake()
        {
            _countDownToBreakControl = new(this, CountdownToBreakControl);
        }

        public void TakeControl(IShapeEntity shapeEntity)
        {
            IsControlEntity = true;
            TakenControl.Invoke();
            _shapeEntity = shapeEntity;
            _shapeEntity.BehaviourModule.SurfaceTouched += _countDownToBreakControl.Start;
            _shapeEntity.Model.Destroyed += BreakControl;
        }

        public void HandleMovingFieldTouchDown(Vector2 touchWorldPosition)
        {
            if (_shapeEntity != null)
            {
                _previousTouchWorldPositionX = touchWorldPosition.x;
            }
        }

        public void HandleMovingFieldTouchMove(Vector2 touchWorldPosition)
        {
            if (_shapeEntity != null)
            {
                float delta = touchWorldPosition.x - _previousTouchWorldPositionX;
                delta = Mathf.Clamp(delta, -_maxShapeXVelocity, _maxShapeXVelocity);
                _shapeEntity.BehaviourModule.MoveHorizontally(delta * _movingFactor);
                _previousTouchWorldPositionX = touchWorldPosition.x;
            }
        }

        private IEnumerator CountdownToBreakControl()
        {
            yield return new WaitForSeconds(_gameRules.ControlDurationAfterSurfaceTouchInSeconds);
            BreakControl();
        }

        private void BreakControl()
        {
            if (_shapeEntity != null)
            {
                _shapeEntity.BehaviourModule.SurfaceTouched -= _countDownToBreakControl.Start;
            }
            _shapeEntity = null;
            _countDownToBreakControl.Stop();
            BreakedControl.Invoke();
            IsControlEntity = false;
        }
    }
}
