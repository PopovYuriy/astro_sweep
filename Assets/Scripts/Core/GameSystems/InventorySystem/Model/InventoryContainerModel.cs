using System;
using System.Collections.Generic;
using System.Linq;
using Core.GameSystems.InventorySystem.Data;

namespace Core.GameSystems.InventorySystem.Model
{
    public class InventoryContainerModel
    {
        private List<ItemModel> _items;

        public int Capacity { get; private set; }
        public int Count => _items.Sum(model => model.Count);
        public bool HasFreeSpace => Count < Capacity;

        public event Action<ItemModel> OnItemAdded;
        public event Action<ItemModel> OnItemPulled;

        public InventoryContainerModel(int capacity)
        {
            _items = new List<ItemModel>(capacity);
            Capacity = capacity;
        }

        public bool TryAddItem(ItemData itemData)
        {
            if (_items.Count == Capacity)
                return false;
            
            var itemModel = _items.FirstOrDefault(model => model.Data.ItemType == itemData.ItemType);
            if (itemModel == null)
            {
                itemModel = new ItemModel(itemData);
                _items.Add(itemModel);
            }
            else
            {
                itemModel.AddItems(1);
            }
                            
            OnItemAdded?.Invoke(itemModel);
            return true;
        }

        public bool TryPullItem(ItemData itemData)
        {
            var itemModel = _items.FirstOrDefault(model => model.Data.ItemType == itemData.ItemType);
            if (itemModel == null)
                return false;
            
            itemModel.PullItem();

            if (itemModel.Count == 0)
                _items.Remove(itemModel);
            
            OnItemPulled?.Invoke(itemModel);
            return true;
        }

        public ItemData GetItem(int index)
        {
            if (index >= _items.Count || index < 0)
                throw new ArgumentOutOfRangeException($"Index is out of range of container items count");
            
            return _items[index].Data;
        }
    }
}