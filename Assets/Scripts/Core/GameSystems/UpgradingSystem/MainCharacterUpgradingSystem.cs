using System;
using System.Collections.Generic;
using Core.GameSystems.InventorySystem;
using Core.GameSystems.InventorySystem.Enums;
using Core.GameSystems.StatsSystem;
using Core.GameSystems.UpgradingSystem.Data;
using Core.GameSystems.UpgradingSystem.Enums;
using Core.GameSystems.UpgradingSystem.Items;
using UnityEngine;
using Zenject;

namespace Core.GameSystems.UpgradingSystem
{
    public sealed class MainCharacterUpgradingSystem : IUpgradingSystem<UpgradableItemId>, IInitializable
    {
        private UpgradingConfigsStorage _upgradingConfigsStorage;
        private CharacterStatsSystem _statsSystem;
        private CharacterInventorySystem _inventorySystem;
        
        private Dictionary<UpgradableItemId, IUpgradableItem> _items;

        [Inject]
        private void Construct(UpgradingConfigsStorage upgradingConfigsStorage, CharacterStatsSystem statsSystem, CharacterInventorySystem inventorySystem)
        {
            _upgradingConfigsStorage = upgradingConfigsStorage;
            _statsSystem = statsSystem;
            _inventorySystem = inventorySystem;
        }

        public void Initialize()
        {
            _items = new Dictionary<UpgradableItemId, IUpgradableItem>(_upgradingConfigsStorage.Items.Length);
            foreach (var upgradableStatConfig in _upgradingConfigsStorage.Items)
            {
                var item = new UpgradableItem();
                item.Initialize(-1, _statsSystem, upgradableStatConfig);
                if (!_items.TryAdd(upgradableStatConfig.Id, item))
                    throw new Exception($"Config has a duplicated item with id {upgradableStatConfig.Id}");
            }
        }

        public IUpgradableItem GetItem(UpgradableItemId id)
        {
            if (_items.TryGetValue(id, out var item))
                return item;

            throw new Exception($"Config has no item with id {id}");
        }

        public void UpgradeItem(UpgradableItemId id)
        {
            var item = GetItem(id);
            
            if (!item.CanUpgrade())
            {
                Debug.LogWarning($"Item {item.Config.Id} can't be upgraded");
                return;
            }
            
            item.Upgrade();

            var ingredients = item.Config.Levels[item.CurrentLevel].Recipe.Ingredients;
            var container = _inventorySystem.GetInventoryContainer(ItemCollectionType.Upgrading);
            foreach (var ingredient in ingredients)
            {
                container.PullItems(ingredient.Type, ingredient.Count);
            }
        }
    }
}