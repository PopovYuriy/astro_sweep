using Core.GameSystems.AbilitySystem.Model;
using Core.GameSystems.InventorySystem;
using Core.GameSystems.InventorySystem.Enums;
using Core.GameSystems.InventorySystem.Model;
using Game.MainCharacter.Abilities.Runners.Common;
using Game.MainCharacter.Input;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities.Runners
{
    public sealed class ThrowAbilityRunner : AbilityRunnerAbstract<ThrowingAbilityModel>
    {
        [SerializeField] private Transform _trajectionStartPosition;
        [SerializeField] private AimController _aimController;

        [Header("Throwing settings")]
        [SerializeField] private float _throwMaxStrength;

        private InventoryContainerModel _inventoryContainer;
        private PlayerInputController _inputController;

        private GameInput _gameInput;

        private Vector3 _currentDirection;
        private Vector3 _currentVelocity;

        private bool _activated;

        [Inject]
        private void Construct(CharacterInventorySystem inventorySystem, PlayerInputController inputController)
        {
            _inventoryContainer = inventorySystem.GetInventoryContainer(ItemCollectionType.Throwable);
            _inputController = inputController;
        }

        private void OnDestroy()
        {
            _inputController.OnFireInput -= FireInputHandler;
        }

        protected override void RunInternal()
        {
            _activated = true;
            _aimController.Activate();
            _inputController.OnFireInput += FireInputHandler;
        }

        protected override void StopInternal()
        {
            _activated = false;
            _aimController.Deactivate();
            _inputController.OnFireInput -= FireInputHandler;
        }
        
        private void FireInputHandler(bool fire)
        {
            if (!fire || !_aimController.IsAiming)
                return;
            
            Throw();
        }

        private void Throw()
        {
            DispatchActed();
            
            var currentObjectData = _inventoryContainer.GetItem(0);
            var objectsRigidbodyToThrow = currentObjectData.Prefab.GetComponent<Rigidbody>();
            
            var throwItem = Instantiate(currentObjectData.Prefab, _trajectionStartPosition.position, Quaternion.identity);
            throwItem.transform.forward = _aimController.AimDirection;
            
            var rb = throwItem.GetComponent<Rigidbody>();
            rb.centerOfMass = throwItem.transform.position;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            var strength = _throwMaxStrength / objectsRigidbodyToThrow.mass;
            var currentVelocity = throwItem.transform.forward * strength;
            rb.AddForce(currentVelocity, ForceMode.Impulse);
            
            _inventoryContainer.PullItems(currentObjectData.ItemType, 1);
            
            Stop();
        }
    }
}