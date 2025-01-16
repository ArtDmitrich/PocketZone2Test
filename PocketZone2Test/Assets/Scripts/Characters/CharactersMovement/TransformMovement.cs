using UnityEngine;

namespace Characters
{
    public class TransformMovement : MonoBehaviour, IMovable
    {
        private IMovementStats _movementStats;
        private Vector2 _direction;
        private bool _isMoving;

        public void Init(IMovementStats movementStats)
        {
            _movementStats = movementStats;
        }

        public void StartMove(Vector2 direction)
        {
            _isMoving = true;
            _direction = direction;
        }

        public void StopMove()
        {
            _isMoving = false;
            _direction = Vector2.zero;
        }

        private void Update()
        {
            if (_isMoving)
            {
                transform.Translate(_direction * _movementStats.MovementSpeed * Time.deltaTime);
            }
        }
    }
}
