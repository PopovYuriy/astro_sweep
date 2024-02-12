using UnityEngine;

namespace Core.GameSystems.InventorySystem.Data
{
    [CreateAssetMenu(fileName = "InventorySystemConfig", menuName = "InventorySystem/InventorySystemConfig", order = 0)]
    public sealed class InventorySystemConfig : ScriptableObject
    {
        [field: SerializeField] public InventoryContainerConfig[] ContainerConfigs { get; private set; }
    }
}