using Services.StateMachine;

namespace GameLogic.Characters
{
    public class DeathState : State<MeleeEnemyCharacter>
    {
        public DeathState(MeleeEnemyCharacter owner) : base(owner) { }

        public override void EnterState()
        {
            _owner.StopMove();
        }

        public override void ExitState() { }

        public override void UpdateState() { }
    }
}
