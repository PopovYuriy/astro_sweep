using System;
using Game.MainCharacter.Input.Enums;
using Game.MainCharacter.Input.InputActionControllers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.MainCharacter.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;

        private GameInput _gameInput;

        private IInputActionController<Vector2> _movementInputAction;
        private IInputActionController<bool> _aimInputAction;
        private IInputActionController<Vector2> _cameraRotationInputAction;
        private IInputActionController<bool> _abilityOneAction;
        private IInputActionController<bool> _abilityTwoAction;
        private IInputActionController<bool> _abilityThreeAction;
        
        public event Action<Vector2> OnMoveInput;
        public event Action<bool> OnAimInput;
        public event Action<Vector2> OnCameraRotationInput;
        public event Action<AbilityButtonId> OnAbilityInput;

        private void OnValidate()
        {
            _playerInput ??= GetComponent<PlayerInput>();
        }

        private void Start()
        {
            _gameInput = new GameInput();

            _movementInputAction = new DirectionInputActionController(_playerInput.actions[_gameInput.Gameplay.Movement.name]);
            _movementInputAction.OnPerformed += MovementActionHandler;

            _aimInputAction = new ButtonInputActionController(_playerInput.actions[_gameInput.Gameplay.Aim.name]);
            _aimInputAction.OnPerformed += AimActionHandler;
            
            _cameraRotationInputAction = new DirectionInputActionController(_playerInput.actions[_gameInput.Gameplay.CameraRotation.name]);
            _cameraRotationInputAction.OnPerformed += CameraRotationActionHandler;

            _abilityOneAction = new ButtonInputActionController(_playerInput.actions[_gameInput.Gameplay.Ability_1.name]);
            _abilityOneAction.OnPerformed += AbilityOneActionHandler;
            
            _abilityTwoAction = new ButtonInputActionController(_playerInput.actions[_gameInput.Gameplay.Ability_2.name]);
            _abilityTwoAction.OnPerformed += AbilityTwoActionHandler;
            
            _abilityThreeAction = new ButtonInputActionController(_playerInput.actions[_gameInput.Gameplay.Ability_3.name]);
            _abilityThreeAction.OnPerformed += AbilityThreeActionHandler;
        }

        private void OnDestroy()
        {
            _movementInputAction.Dispose();
            _aimInputAction.Dispose();
            _cameraRotationInputAction.Dispose();
            _abilityOneAction.Dispose();
            _abilityTwoAction.Dispose();
            _abilityThreeAction.Dispose();
            
            _movementInputAction.OnPerformed -= MovementActionHandler;
            _aimInputAction.OnPerformed -= AimActionHandler;
            _cameraRotationInputAction.OnPerformed -= CameraRotationActionHandler;
            _abilityOneAction.OnPerformed -= AbilityOneActionHandler;
            _abilityTwoAction.OnPerformed -= AbilityTwoActionHandler;
            _abilityThreeAction.OnPerformed -= AbilityThreeActionHandler;
        }

        private void MovementActionHandler(Vector2 direction)
        {
            OnMoveInput?.Invoke(direction);
        }
        
        private void AimActionHandler(bool isPressed)
        {
            OnAimInput?.Invoke(isPressed);
        }
        
        private void CameraRotationActionHandler(Vector2 direction)
        {
            OnCameraRotationInput?.Invoke(direction);
        }

        private void AbilityOneActionHandler(bool isPressed)
        {
            if (isPressed)
                OnAbilityInput?.Invoke(AbilityButtonId.Ability_1);
        }
        
        private void AbilityTwoActionHandler(bool isPressed)
        {
            if (isPressed)
                OnAbilityInput?.Invoke(AbilityButtonId.Ability_2);
        }
        
        private void AbilityThreeActionHandler(bool isPressed)
        {
            if (isPressed)
                OnAbilityInput?.Invoke(AbilityButtonId.Ability_3);
        }
    }
}