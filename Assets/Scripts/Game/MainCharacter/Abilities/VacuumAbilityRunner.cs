using System.Collections.Generic;
using Core.GameSystems.InventorySystem;
using Core.GameSystems.InventorySystem.Enums;
using Core.GameSystems.InventorySystem.Model;
using Game.Environment.InteractiveItems;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities
{
    public sealed class VacuumAbilityRunner : AbilityRunnerAbstract
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private float _suckingStrength;
        [SerializeField] private float _minDistanceToPickUp;

        private InventoryContainerModel _inventoryContainer;
        
        private bool _activated;
        private List<VacuumableItem> _objectsToVacuuming;
        private List<VacuumableItem> _objectsToAdd;
        private List<VacuumableItem> _objectsToDelete;

        [Inject]
        private void Construct(CharacterInventorySystem characterInventorySystem)
        {
            _inventoryContainer = characterInventorySystem.GetInventoryContainer(ItemCollectionType.Throwable);
        }

        private void Start()
        {
            _objectsToVacuuming = new List<VacuumableItem>();
            _objectsToDelete = new List<VacuumableItem>();
            _objectsToAdd = new List<VacuumableItem>();
            Stop();
        }

        private void Update()
        {
            if (!_activated)
                return;
            
            if (_objectsToAdd.Count > 0)
            {
                foreach (var vacuumableItem in _objectsToAdd)
                    _objectsToVacuuming.Add(vacuumableItem);
                
                _objectsToAdd.Clear();
            }
            
            if (_objectsToVacuuming.Count > 0)
            {
                foreach (var vacuumableItem in _objectsToVacuuming)
                    Vacuum(vacuumableItem);
            }

            if (_objectsToDelete.Count > 0)
            {
                foreach (var vacuumableItem in _objectsToDelete)
                    _objectsToVacuuming.Remove(vacuumableItem);
                
                _objectsToDelete.Clear();
            }
            
            if (!_inventoryContainer.HasFreeSpace)
                Stop();
        }

        protected override void RunInternal()
        {
            _activated = true;
            _collider.enabled = true;
            Debug.Log("Vacuuming started");
        }

        protected override void StopInternal()
        {
            _activated = false;
            _objectsToVacuuming.Clear();
            _objectsToAdd.Clear();
            _objectsToDelete.Clear();
            
            _collider.enabled = false;
            Debug.Log("Vacuuming stopped");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out VacuumableItem item) && item.Data.CollectionType == ItemCollectionType.Throwable)
                _objectsToAdd.Add(item);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out VacuumableItem item) && _objectsToVacuuming.Contains(item))
                _objectsToDelete.Add(item);
        }

        private void Vacuum(VacuumableItem item)
        {
            var distance = Vector3.Distance(transform.position, item.transform.position);
            
            if (distance < _minDistanceToPickUp && _inventoryContainer.HasFreeSpace)
            {
                _inventoryContainer.TryAddItem(item.Data);
                Destroy(item.gameObject);
                
                if (!_inventoryContainer.HasFreeSpace)
                    _objectsToDelete.Add(item);
            }
            else
            {
                var forceDirection = (transform.position - item.transform.position).normalized;
                var force = forceDirection * (_suckingStrength / distance);
                item.Rigidbody.AddForce(force, ForceMode.VelocityChange);
            }
        }
    }
}