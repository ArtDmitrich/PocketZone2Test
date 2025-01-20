using Services.StateMachine;
using UnityEngine;

namespace GameLogic.Characters
{
    public class FollowState : State<MeleeEnemyCharacter>
    {
        public FollowState(MeleeEnemyCharacter owner) : base(owner) { }
        
        public override void EnterState() { }

        public override void UpdateState()
        {
            if (!_owner.CheckNearestTarget())
            {
                _owner.ChangeState(new IdleState(_owner));
            }
            else if (_owner.CheckAttackDistance())
            {
                _owner.ChangeState(new AttackState(_owner));
            }
            else 
            {
                _owner.StartMoveToTarget();
            }
        }

        public override void ExitState()
        {
            _owner.StopMove();
        }
    }
}
