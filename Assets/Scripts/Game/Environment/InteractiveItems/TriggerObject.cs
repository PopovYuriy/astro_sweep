using Game.Environment.InteractiveItems.Enums;
using UnityEngine;

namespace Game.Environment.InteractiveItems
{
    public class TriggerObject : MonoBehaviour
    {
        [field: SerializeField] public TriggerItemId Id { get; private set; }
    }
}