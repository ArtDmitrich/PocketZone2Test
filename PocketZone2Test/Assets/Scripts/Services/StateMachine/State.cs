using UnityEngine;

namespace Services.StateMachine
{
    public abstract class State<T> where T : MonoBehaviour
    {
        protected T _owner;
        
        protected State(T owner)
        {
            _owner = owner;
        }
        
        public abstract void EnterState();

        public abstract void ExitState();

        public abstract void UpdateState();
    }
}
