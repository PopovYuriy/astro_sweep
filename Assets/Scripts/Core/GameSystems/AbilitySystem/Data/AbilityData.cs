using System;
using Core.GameSystems.AbilitySystem.Enums;
using Core.GameSystems.StatsSystem.Enum;
using Tools.Unity.EnumFlags;
using UnityEngine;

namespace Core.GameSystems.AbilitySystem.Data
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "Abilities/AbilityData", order = 0)]
    public sealed class AbilityData : ScriptableObject
    {
        [field: SerializeField] public AbilityType Type { get; private set; }
        [field: SerializeField] public EnumFlags<AbilityType> AbilitiesToStop { get; private set; }
        [field: SerializeField] public EnumFlags<AbilityType> AbilitiesToBlock { get; private set; }
        [field: SerializeField] public ChargingData ChargingData { get; private set; }
    }
    
    [Serializable]
    public sealed class ChargingData
    {
        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public ModifierType ModifierType { get; private set; }
    }
}