using UnityEngine;

namespace Services.StateMachine
{
    public abstract class StateMachine<T> where T : MonoBehaviour
    {
        protected T _owner;
        private State<T> _currentState;

        protected StateMachine(T owner)
        {
            _owner = owner;
        }
        
        public virtual void ChangeState(State<T> state)
        {
            _currentState?.ExitState();

            _currentState = state;
            _currentState.EnterState();
        }

        public virtual void UpdateState()
        {
            _currentState?.UpdateState();
        }
    }
}
