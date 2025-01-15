using System;
using Services.Initialize;
using UnityEngine;

namespace Services.Input
{
    public class InputService : IInputService, IInitialazable, IDisposable
    {
        public event Action<Vector2> PlayerMoveStarted;
        public event Action PlayerMoveStoped;

        private InputActions _inputActions;

        public void Init()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
        
            _inputActions.CharacterInput.Move.performed += PlayerMoveStart;
            _inputActions.CharacterInput.Move.canceled += PlayerMoveStop;
        }

        public void Dispose()
        {
            _inputActions.CharacterInput.Move.performed -= PlayerMoveStart;
            _inputActions.CharacterInput.Move.canceled -= PlayerMoveStop;
        }
    
        private void PlayerMoveStart(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            PlayerMoveStarted?.Invoke(ctx.ReadValue<Vector2>());
        }

        private void PlayerMoveStop(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            PlayerMoveStoped?.Invoke();
        }
    }
}
