using UnityEngine;
using UnityEngine.Events;

namespace LiquidTetris.NetworkCode
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _shapesRoot;

        [Header("Events:")]
        [SerializeField] private UnityEvent _sendingAllowed;
        [SerializeField] private UnityEvent _sendingDenied;

        public UnityEvent SendingAllowed => _sendingAllowed;
        public UnityEvent SendingDenied => _sendingDenied;

        public void PlaceEntityOnSpawnPoint(ISpawnable spawnableEntity, Vector2 offset = default)
        {
            spawnableEntity.transform.parent = _shapesRoot;
            spawnableEntity.transform.position = new Vector3(
                _spawnPoint.position.x + offset.x,
                _spawnPoint.position.y + offset.y,
                _spawnPoint.position.z);
        }
    }
}
