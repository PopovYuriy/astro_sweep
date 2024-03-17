using Core.GameSystems.InventorySystem.Data;
using UnityEngine;

namespace Core.GameSystems.InventorySystem.Model
{
    public class ItemModel
    {
        public ItemData Data { get; }
        public int Count { get; private set; }

        public ItemModel(ItemData data)
        {
            Data = data;
            Count = 1;
        }

        public void AddItems(int count)
        {
            if (count <= 0)
            {
                Debug.LogError("Count must be more than 0");
                return;
            }

            Count += count;
        }

        public void PullItems(int count)
        {
            var newCount = Count - count;
            if (newCount < 0)
            {
                Debug.LogError("Can't pull items. New count is less then 0");
                return;
            }

            Count -= count;
        }
    }
}