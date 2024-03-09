using System;
using UnityEngine.InputSystem;

namespace Game.MainCharacter.Input.InputActionControllers
{
    public sealed class ButtonInputActionController : IInputActionController<bool>
    {
        private readonly InputAction _inputAction;

        public event Action<bool> OnPerformed;

        public ButtonInputActionController(InputAction inputAction)
        {
            _inputAction = inputAction;
            _inputAction.started += ActionStartedHandler;
            _inputAction.canceled += ActionCanceledHandler;
        }

        public void Dispose()
        {
            _inputAction.started -= ActionStartedHandler;
            _inputAction.canceled -= ActionCanceledHandler;
        }

        private void ActionStartedHandler(InputAction.CallbackContext obj)
        {
            OnPerformed?.Invoke(true);
        }
        
        private void ActionCanceledHandler(InputAction.CallbackContext obj)
        {
            OnPerformed?.Invoke(false);
        }
    }
}