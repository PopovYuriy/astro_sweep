using System;
using System.Linq;
using Core.GameSystems.AbilitySystem.Enums;
using Game.MainCharacter.Input;
using Game.MainCharacter.Input.Enums;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities.Detectors
{
    public sealed class AbilityRunButtonPressDetector : MonoBehaviour
    {
        [SerializeField] private KeyCodeToAbilityMap[] _keysMap;
        [SerializeField] private AbilityRunController _abilitiesController;

        private PlayerInputController _inputController;

        [Inject]
        private void Construct(PlayerInputController inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            _inputController.OnAbilityInput += AbilityInputHandler;
        }
        
        private void OnDestroy()
        {
            _inputController.OnAbilityInput -= AbilityInputHandler;
        }

        private void AbilityInputHandler(AbilityButtonId buttonId)
        {
            var abilityMap = _keysMap.FirstOrDefault(k => k.AbilityButtonId == buttonId);
            if (abilityMap == null)
            {
                Debug.LogWarning($"Here is no binded ability for key {buttonId}");
                return;
            }
            
            _abilitiesController.ProcessAbilityRunner(abilityMap.AbilityType);
        }
        
        [Serializable]
        private sealed class KeyCodeToAbilityMap
        {
            [field: SerializeField] public AbilityButtonId AbilityButtonId { get; private set; }
            [field: SerializeField] public AbilityType AbilityType { get; private set; }
        }
    }
}