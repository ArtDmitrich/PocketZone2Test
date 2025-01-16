using Services.Initialize;
using Services.Input;
using Services.UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class GameLoopBootstrap : MonoBehaviour
    {
        private IMediatorUI _mediator;
        private IInputService _inputService;
    
        [Inject]
        private void Construct(IMediatorUI mediator, IInputService inputService)
        {
            _mediator = mediator;
            _inputService = inputService;

            if (_inputService is IInitialazable input)
            {
                input.Init();
            }
        }
        void Start()
        {
            //state machine -> start state
        }
    }
}
