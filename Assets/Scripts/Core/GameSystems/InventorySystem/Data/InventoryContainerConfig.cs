using Core.GameSystems.InventorySystem.Enums;
using UnityEngine;

namespace Core.GameSystems.InventorySystem.Data
{
    [CreateAssetMenu(fileName = "InventoryContainerConfig", menuName = "InventorySystem/InventoryContainerConfig", order = 0)]
    public sealed class InventoryContainerConfig : ScriptableObject
    {
        [field: SerializeField] public ItemCollectionType Type { get; private set; }
        [field: SerializeField] public int Capacity { get; private set; }
    }
}