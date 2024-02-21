using System;
using Core.GameSystems.AbilitySystem.Data;
using Core.GameSystems.InventorySystem;
using Core.GameSystems.InventorySystem.Enums;
using Core.GameSystems.InventorySystem.Model;
using Zenject;

namespace Core.GameSystems.AbilitySystem.Model
{
    public sealed class ThrowingAbilityModel : IAbilityModel
    {
        private InventoryContainerModel _characterInventoryContainer;
        private bool _isInitialized;
        
        public AbilityData Data { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsReady { get; private set; }

        public event Action OnReadyChanged;

        [Inject]
        private void Construct(CharacterInventorySystem characterInventorySystem)
        {
            _characterInventoryContainer = characterInventorySystem.GetInventoryContainer(ItemCollectionType.Throwable);
        }

        public void Initialize(AbilityData data)
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
            
            Data = data;
            IsAvailable = true;
            DetermineIsReady(false);
            
            _characterInventoryContainer.OnItemAdded += InventoryItemAddedHandler;
            _characterInventoryContainer.OnItemPulled += InventoryItemAPulledHandler;
        }

        public void Dispose()
        {
            _characterInventoryContainer.OnItemAdded -= InventoryItemAddedHandler;
            _characterInventoryContainer.OnItemPulled -= InventoryItemAPulledHandler;
        }

        private void InventoryItemAddedHandler(ItemModel item)
        {
            DetermineIsReady(true);
        }
        
        private void InventoryItemAPulledHandler(ItemModel item)
        {
            DetermineIsReady(true);
        }

        private void DetermineIsReady(bool needDispatchEvent)
        {
            var previousValue = IsReady;
            IsReady = _characterInventoryContainer.Count > 0;
            
            if (needDispatchEvent && previousValue != IsReady)
                OnReadyChanged?.Invoke();
        }
    }
}