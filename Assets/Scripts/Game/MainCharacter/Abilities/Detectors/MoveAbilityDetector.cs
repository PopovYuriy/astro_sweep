using Core.GameSystems.AbilitySystem;
using Core.GameSystems.AbilitySystem.Enums;
using Core.GameSystems.AbilitySystem.Model;
using Game.MainCharacter.Input;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities.Detectors
{
    public sealed class MoveAbilityDetector : MonoBehaviour
    {
        [SerializeField] private AbilityRunController _abilitiesController;
        
        private MovingAbilityModel _movingAbilityModel;
        private PlayerInputController _inputController;

        [Inject]
        private void Construct(CharacterAbilitySystem abilitySystem, PlayerInputController inputController)
        {
            _movingAbilityModel = (MovingAbilityModel) abilitySystem.GetAbilityModel(AbilityType.Moving);
            _inputController = inputController;
        }

        private void Start()
        {
            _inputController.OnMoveInput += MoveInputHandler;
        }

        private void OnDestroy()
        {
            _inputController.OnMoveInput -= MoveInputHandler;
        }

        private void MoveInputHandler(Vector2 direction)
        {
            _movingAbilityModel.MoveDirection = direction;
            
            if (direction == Vector2.zero)
            {
                if (_abilitiesController.IsAbilityRan(AbilityType.Moving))
                    _abilitiesController.ProcessAbilityRunner(AbilityType.Moving);
            }
            else
            {
                if (!_abilitiesController.IsAbilityRan(AbilityType.Moving))
                    _abilitiesController.ProcessAbilityRunner(AbilityType.Moving);
            }
        }
    }
}