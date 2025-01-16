using UnityEngine;

namespace Characters
{
    public class MovableCharacter : Character
    {
        private IMovable Movement { get { return _movement ??= GetComponent<IMovable>(); } }
        private IMovable _movement;

        protected void StartMovement(Vector2 direction)
        {
            Movement.StartMove(direction);
            Model.PlayLoopAnimation(ModelAnimation.Walk, true);
        }

        protected void StopMovement()
        {
            Movement.StopMove();
            Model.PlayLoopAnimation(ModelAnimation.Walk, false);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            Movement.Init(Stats);
        }
    }
}
