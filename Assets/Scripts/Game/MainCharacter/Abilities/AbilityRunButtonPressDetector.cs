using System;
using System.Linq;
using Core.GameSystems.AbilitySystem.Enums;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Game.MainCharacter.Abilities
{
    public sealed class AbilityRunButtonPressDetector : MonoBehaviour
    {
        [SerializeField] private KeyCodeToAbilityMap[] _keysMap;
        [SerializeField] private AbilityRunController _abilitiesController;

        private GameInput _gameInput;

        private void Awake()
        {
            _gameInput = new GameInput();
            _gameInput.Enable();
            _gameInput.Gameplay.Abilities.performed += AbilitiesOnPerformed;
        }

        private void OnDestroy()
        {
            _gameInput.Gameplay.Abilities.performed -= AbilitiesOnPerformed;
        }

        private void AbilitiesOnPerformed(InputAction.CallbackContext obj)
        {
            var keyControl = (KeyControl) obj.control;
            var abilityMap = _keysMap.FirstOrDefault(k => k.KeyCode == keyControl.keyCode);
            if (abilityMap == null)
            {
                Debug.LogWarning($"Here is no binded ability for key {keyControl.keyCode}");
                return;
            }
            
            _abilitiesController.TryRunAbility(abilityMap.AbilityType);
        }
        
        [Serializable]
        private sealed class KeyCodeToAbilityMap
        {
            [field: SerializeField] public Key KeyCode { get; private set; }
            [field: SerializeField] public AbilityType AbilityType { get; private set; }
        }
    }
}