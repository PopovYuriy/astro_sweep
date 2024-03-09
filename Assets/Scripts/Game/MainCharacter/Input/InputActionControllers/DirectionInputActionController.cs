using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.MainCharacter.Input.InputActionControllers
{
    public sealed class DirectionInputActionController : IInputActionController<Vector2>
    {
        private readonly InputAction _inputAction;

        public event Action<Vector2> OnPerformed;

        public DirectionInputActionController(InputAction inputAction)
        {
            _inputAction = inputAction;
            _inputAction.performed += ActionPerformedHandler;
            _inputAction.canceled += ActionCanceledHandler;
        }

        public void Dispose()
        {
            _inputAction.performed -= ActionPerformedHandler;
            _inputAction.canceled -= ActionCanceledHandler;
        }

        private void ActionPerformedHandler(InputAction.CallbackContext obj)
        {
            OnPerformed?.Invoke(obj.ReadValue<Vector2>());
        }
        
        private void ActionCanceledHandler(InputAction.CallbackContext obj)
        {
            OnPerformed?.Invoke(Vector2.zero);
        }
    }
}