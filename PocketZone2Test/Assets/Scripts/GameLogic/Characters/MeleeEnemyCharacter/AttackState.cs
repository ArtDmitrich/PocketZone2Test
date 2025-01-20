using Services.StateMachine;
using UnityEngine;

namespace GameLogic.Characters
{
    public class AttackState : State<MeleeEnemyCharacter>
    {
        public AttackState(MeleeEnemyCharacter owner) : base(owner) { }

        public override void EnterState() { }

        public override void UpdateState()
        {
            if (_owner.CheckAttackPosibility())
            {
                _owner.StartAttack();
            }
            else
            {
                _owner.ChangeState(new IdleState(_owner));
            }
        }
        
        public override void ExitState() { }

    }
}
