using Core.GameSystems.InventorySystem.Data;
using UnityEngine;

namespace Game.Environment.InteractiveItems
{
    public class InventoryItem : MonoBehaviour
    {
        [field: SerializeField] public ItemData Data { get; private set; }
    }
}