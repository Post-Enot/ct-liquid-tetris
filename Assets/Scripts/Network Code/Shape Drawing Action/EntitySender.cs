using CoroutineShells;
using LiquidTetris.SpecificCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class EntitySender : MonoBehaviour
    {
        [SerializeField] private GameRules _gameRules;
        [SerializeField] private GameField _gameField;
        [SerializeField] private EntityController _entityController;

        public bool IsShapeSendingAllowed { get; private set; } = true;

        public event Action ShapeSendingAllowed;
        public event Action ShapeSendingProhibited;

        private bool _isEntityTakeQueue;
        private bool _isEntitySendingPaused;
        private ISpawnable _lastSentEntity;
        private UniqueCoroutine<float> _pauseBetweenEntitySendingRoutine;
        private BorderedQueue<NetworkShapeEntity> _shapesQueue;
        private readonly Queue<ISpawnable> _entitiesQueue = new();

        private void Awake()
        {
            _pauseBetweenEntitySendingRoutine = new(this, PauseBetweenEntitySending);
            _shapesQueue = new(_gameRules.ShapeQueueMaxElemenetCount);
            EnableMonitoring();
        }

        private void OnDestroy()
        {
            DisableMonitoring();
        }

        public void SendShape(NetworkShapeEntity networkShapeEntity)
        {
            if (IsShapeSendingAllowed)
            {
                _gameField.PlaceEntityOnSpawnPoint(networkShapeEntity.Spawnable);
                if (networkShapeEntity.Spawnable.IsTakesQueue)
                {
                    InitLastSentEntity(networkShapeEntity.Spawnable);
                }
                _pauseBetweenEntitySendingRoutine.Start(_gameRules.PauseBetweenSendingInSeconds);
                _entityController.TakeControl(networkShapeEntity);
            }
            else
            {
                throw new InvalidOperationException("Try to send shape without permission");
            }
        }

        public void SendEntity(ISpawnable spawnableEntity)
        {

        }

        private void InitLastSentEntity(ISpawnable spawnable)
        {
            _isEntityTakeQueue = true;
            spawnable.FreeUpQueueSpace += HandleQueueRelease;
            _lastSentEntity = spawnable;
        }

        private void EnableMonitoring()
        {
            _entityController.BreakedControl.AddListener(CheckShapeSendingPossibility);
        }

        private void DisableMonitoring()
        {
            _entityController.BreakedControl.RemoveListener(CheckShapeSendingPossibility);
        }

        private void HandleQueueRelease()
        {
            _isEntityTakeQueue = false;
            _lastSentEntity.FreeUpQueueSpace -= HandleQueueRelease;
            CheckShapeSendingPossibility();
        }

        private void CheckShapeSendingPossibility()
        {
            if (!IsShapeSendingAllowed
                && !_isEntitySendingPaused
                && !_isEntityTakeQueue
                && !_entityController.IsControlEntity)
            {
                if (_shapesQueue.Count > 0 || _entitiesQueue.Count > 0)
                {
                    AdvanceQueue();
                }
                else
                {
                    IsShapeSendingAllowed = true;
                    ShapeSendingAllowed.Invoke();
                }
            }
        }

        private void AdvanceQueue()
        {
            if (_entitiesQueue.Count > 0)
            {
                ISpawnable spawnable = _entitiesQueue.Dequeue();
                SendEntity(spawnable);
            }
            else if (_shapesQueue.Count > 0)
            {
                NetworkShapeEntity networkShapeEntity = _shapesQueue.Dequeue();
                SendShape(networkShapeEntity);
            }
        }

        private IEnumerator PauseBetweenEntitySending(float pauseDurationInSeconds)
        {
            _isEntitySendingPaused = true;
            while (pauseDurationInSeconds > 0)
            {
                yield return null;
                pauseDurationInSeconds -= Time.deltaTime;
            }
            _isEntitySendingPaused = false;
            CheckShapeSendingPossibility();
        }
    }
}
