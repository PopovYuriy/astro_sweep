using System.Collections.Generic;
using Core.GameSystems.InventorySystem;
using Core.GameSystems.InventorySystem.Enums;
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

        private CharacterInventorySystem _characterInventorySystem;
        
        private bool _enabled;
        private List<VacuumableItem> _objectsToVacuuming;

        [Inject]
        private void Construct(CharacterInventorySystem characterInventorySystem)
        {
            _characterInventorySystem = characterInventorySystem;
        }

        private void Start()
        {
            _objectsToVacuuming = new List<VacuumableItem>();
            Stop();
        }

        private void Update()
        {
            if (_objectsToVacuuming.Count > 0)
            {
                foreach (var vacuumableItem in _objectsToVacuuming)
                {
                    Vacuum(vacuumableItem);
                }
            }
        }

        public override void Run()
        {
            _collider.enabled = true;
        }

        public override void Stop()
        {
            _objectsToVacuuming.Clear();
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out VacuumableItem item) && item.Data.CollectionType == ItemCollectionType.Throwable)
                _objectsToVacuuming.Add(item);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out VacuumableItem item) && _objectsToVacuuming.Contains(item))
                _objectsToVacuuming.Remove(item);
        }

        private void Vacuum(VacuumableItem item)
        {
            var forceDirection = (transform.position - item.transform.position).normalized;
            var distance = Vector3.Distance(transform.position, item.transform.position);

            if (distance < _minDistanceToPickUp)
            {
                var inventoryContainer = _characterInventorySystem.GetInventoryContainer(item.Data.CollectionType);
                inventoryContainer.TryAddItem(item.Data);
                Destroy(item.gameObject);
                
                if (!inventoryContainer.HasFreeSpace)
                    Stop();
            }
            else
            {
                var force = forceDirection * (_suckingStrength / distance);
                item.Rigidbody.AddForce(force, ForceMode.VelocityChange);
            }
        }
    }
}