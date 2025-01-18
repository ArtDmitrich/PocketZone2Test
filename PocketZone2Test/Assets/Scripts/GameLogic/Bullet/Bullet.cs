using GameLogic.Characters;
using Services.ObjectPool;
using UnityEngine;
using Weapons;

namespace GameLogic.Bullet
{
    [RequireComponent(typeof(PooledItem))]
    public class Bullet : MonoBehaviour, IMovementStats
    {
        public float MovementSpeed => _movementSpeed;
        
        [SerializeField] private float _movementSpeed;
        
        private IMovable Movement { get { return _movement ??= GetComponent<IMovable>(); } }
        private IMovable _movement;
        
        private PooledItem PooledItem { get { return _pooledItem ??= GetComponent<PooledItem>(); } }
        private PooledItem _pooledItem;
        
        private int _bulletDamage;
        private TargetType _targetType;

        public void Initialization(int damage, TargetType target)
        {
            _bulletDamage = damage;
            _targetType = target;
            
            Movement.Init(this);
        }

        public void StartMovement(Vector2 direction)
        {
            Movement.StartMove(direction);
        }
        
        private void TakeDamage(ITakingDamage target)
        {
            target.TakeDamage(_bulletDamage);
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            Movement.StopMove();
            
            if (other.gameObject.CompareTag(_targetType.ToString()) &&
                other.gameObject.TryGetComponent<ITakingDamage>(out var target))
            {
                TakeDamage(target);
            }
            
            Deactivate();
        }

        private void Deactivate()
        {
            if (gameObject.activeInHierarchy)
            {
                PooledItem.Release();
            }
        }
    }
}
