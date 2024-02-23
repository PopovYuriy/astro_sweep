using Core.GameSystems.AbilitySystem;
using Core.GameSystems.AbilitySystem.Enums;
using Core.GameSystems.AbilitySystem.Model;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities.Detectors
{
    public sealed class MoveAbilityDetector : MonoBehaviour
    {
        [SerializeField] private AbilityRunController _abilitiesController;
        
        private MovingAbilityModel _movingAbilityModel;
        
        private GameInput _gameInput;
        private Vector2 _previousDirection;

        [Inject]
        private void Construct(CharacterAbilitySystem abilitySystem)
        {
            _movingAbilityModel = (MovingAbilityModel) abilitySystem.GetAbilityModel(AbilityType.Moving);
        }

        private void Awake()
        {
            _gameInput = new GameInput();
            _gameInput.Enable();
        }

        private void FixedUpdate()
        {
            var moveDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
            
            if (moveDirection == _previousDirection)
                return;

            if (moveDirection == Vector2.zero)
            {
                _abilitiesController.ProcessAbilityRunner(AbilityType.Moving);
                _movingAbilityModel.MoveDirection = Vector2.zero;
                _previousDirection = Vector2.zero;
            }
            else if (_previousDirection == Vector2.zero)
            {
                _abilitiesController.ProcessAbilityRunner(AbilityType.Moving);
            }
            
            _previousDirection = moveDirection;
            _movingAbilityModel.MoveDirection = moveDirection;
        }
    }
}