using Core.GameSystems.AbilitySystem.Enums;
using UnityEngine;

namespace Core.GameSystems.AbilitySystem.Data
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "Abilities/AbilityData", order = 0)]
    public sealed class AbilityData : ScriptableObject
    {
        [field: SerializeField] public AbilityType Type { get; private set; }
        [field: SerializeField] public int ChargeCost { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
    }
}