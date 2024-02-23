using Core.GameSystems.AbilitySystem.Model;
using Core.GameSystems.InventorySystem;
using Core.GameSystems.InventorySystem.Enums;
using Core.GameSystems.InventorySystem.Model;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities.Runners
{
    public sealed class ThrowAbilityRunner : AbilityRunnerAbstract<ThrowingAbilityModel>
    {
        [SerializeField] private Transform _trajectionStartPosition;

        [Header("Throwing settings")]
        [SerializeField] private float _throwMaxStrength;
        [SerializeField] [Range(0, 90)] private float _minAngle;
        [SerializeField] [Range(0, 90)] private float _maxAngle;

        private InventoryContainerModel _inventoryContainer;

        private GameInput _gameInput;

        private Vector3 _currentDirection;
        private Vector3 _currentVelocity;

        private bool _activated;

        [Inject]
        private void Construct(CharacterInventorySystem inventorySystem)
        {
            _inventoryContainer = inventorySystem.GetInventoryContainer(ItemCollectionType.Throwable);
        }

        protected override void RunInternal()
        {
            _activated = true;
        }

        protected override void StopInternal()
        {
            _activated = false;
        }

        private void Update()
        {
            if (!_activated)
                return;

            _currentDirection = DetermineDirection(_minAngle);
            
            if (UnityEngine.Input.GetKeyUp(KeyCode.Space))
                Throw();
        }

        private Vector3 DetermineDirection(float angle)
        {
            var sign = Mathf.Sign(transform.forward.z) > 0 ? -1 : 1; 
            return (Quaternion.Euler(sign * angle, 0, 0) *  transform.forward).normalized;
        }

        private void Throw()
        {
            DispatchActed();
            
            var currentObjectData = _inventoryContainer.GetItem(0);
            var objectsRigidbodyToThrow = currentObjectData.Prefab.GetComponent<Rigidbody>();
            
            var throwItem = Instantiate(currentObjectData.Prefab, _trajectionStartPosition.position, Quaternion.identity);
            var rb = throwItem.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            var strength = _throwMaxStrength / objectsRigidbodyToThrow.mass;
            var currentVelocity = _currentDirection * strength;
            rb.AddForce(currentVelocity, ForceMode.Impulse);
            
            _inventoryContainer.TryPullItem(currentObjectData);
            
            Stop();
        }
    }
}