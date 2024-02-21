using Core.GameSystems.AbilitySystem.Enums;
using UnityEngine;

namespace Game.Environment.InteractiveItems
{
    [RequireComponent(typeof(Collider))]
    public sealed class AbilityPlaceholder : MonoBehaviour
    {
        [field: SerializeField] public AbilityType AbilityType { get; private set; }
    }
}