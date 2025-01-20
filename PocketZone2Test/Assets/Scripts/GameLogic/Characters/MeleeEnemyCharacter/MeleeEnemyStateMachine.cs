using Services.StateMachine;

namespace GameLogic.Characters
{
    public class MeleeEnemyStateMachine : StateMachine<MeleeEnemyCharacter>
    {
        public MeleeEnemyStateMachine(MeleeEnemyCharacter owner) : base(owner) { }
    }
}
