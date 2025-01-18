using UnityEngine;

namespace GameLogic.Characters
{
    public class MovableCharacter : Character
    {
        private IMovable Movement { get { return _movement ??= GetComponent<IMovable>(); } }
        private IMovable _movement;

        protected void StartMovement(Vector2 direction)
        {
            var targetRotation = direction.x < 0 ? CharacterDirection.Left : CharacterDirection.Right;
            CharacterRotator.RotateTo(targetRotation, Model.transform);
            
            Movement.StartMove(direction);
            Model.PlayLoopAnimation(ModelAnimation.Walk, true);
        }

        protected void StopMovement()
        {
            Movement.StopMove();
            Model.PlayLoopAnimation(ModelAnimation.Walk, false);
        }
        
        protected override void Initialization()
        {
            base.Initialization();
            
            Movement.Init(Stats);
        }
    }
}
