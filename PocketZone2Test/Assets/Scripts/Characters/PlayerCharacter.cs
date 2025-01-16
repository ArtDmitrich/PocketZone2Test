using Services.Input;
using UnityEngine.PlayerLoop;

namespace Characters
{
    public class PlayerCharacter : MovableCharacter
    {
        private IInputService _inputService;

        public void Initialization(IInputService inputService)
        {
            _inputService = inputService;

            _inputService.PlayerMoveStarted += StartMovement;
            _inputService.PlayerMoveStoped += StopMovement;
        }

    }
}
