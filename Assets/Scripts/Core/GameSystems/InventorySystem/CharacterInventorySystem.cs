using System;
using System.Collections.Generic;
using Core.GameSystems.InventorySystem.Data;
using Core.GameSystems.InventorySystem.Enums;
using Core.GameSystems.InventorySystem.Model;
using Zenject;

namespace Core.GameSystems.InventorySystem
{
    public class CharacterInventorySystem
    {
        private InventorySystemConfig _config;
        
        private Dictionary<ItemCollectionType, InventoryContainerModel> _containersMap;

        [Inject]
        private void Construct(InventorySystemConfig config)
        {
            _config = config;

            _containersMap = new Dictionary<ItemCollectionType, InventoryContainerModel>(_config.ContainerConfigs.Length);

            foreach (var containerConfig in _config.ContainerConfigs)
                _containersMap.Add(containerConfig.Type, new InventoryContainerModel(containerConfig.Capacity));
        }

        public InventoryContainerModel GetInventoryContainer(ItemCollectionType itemCollectionType)
        {
            if (!_containersMap.TryGetValue(itemCollectionType, out var containerModel))
            {
                throw new ArgumentException($"Container for item's type {itemCollectionType} is not found.");
            }

            return containerModel;
        }
    }
}