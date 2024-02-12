using UnityEngine;

namespace Game.Environment.InteractiveItems
{
    [RequireComponent(typeof(Rigidbody))]
    public class SuckableItem : InventoryItem
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        private void OnValidate()
        {
            Rigidbody ??= GetComponent<Rigidbody>();
        }
    }
}