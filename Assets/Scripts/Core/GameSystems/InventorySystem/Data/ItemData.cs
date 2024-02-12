using Core.GameSystems.InventorySystem.Enums;
using UnityEngine;

namespace Core.GameSystems.InventorySystem.Data
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "InventorySystem/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public ItemCollectionType CollectionType { get; private set; }
        [field: SerializeField] public ItemType ItemType { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public Sprite Preview { get; private set; }
    }
}