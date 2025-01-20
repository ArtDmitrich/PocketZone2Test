using Services.StateMachine;

namespace GameLogic.Characters
{
    public class IdleState : State<MeleeEnemyCharacter>
    {
        public IdleState(MeleeEnemyCharacter owner) : base(owner) { }

        public override void EnterState()
        {
            _owner.StopMove();
        }
    
        public override void UpdateState()
        {
            if (_owner.CheckNearestTarget())
            {
                _owner.ChangeState(new FollowState(_owner));
            }
        }
    
        public override void ExitState() { }
    }
}
