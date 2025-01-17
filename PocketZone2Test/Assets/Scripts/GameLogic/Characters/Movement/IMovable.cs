using UnityEngine;

namespace Characters
{
    public interface IMovable
    {
        void Init(IMovementStats movementStats);
        void StartMove(Vector2 direction);
        void StopMove();
    }
}
