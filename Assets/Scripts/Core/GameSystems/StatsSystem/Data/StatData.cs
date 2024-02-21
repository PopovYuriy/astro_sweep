using Core.GameSystems.StatsSystem.Enum;
using UnityEngine;

namespace Core.GameSystems.StatsSystem.Data
{
    [CreateAssetMenu(fileName = "StatData", menuName = "StatsSystem/StatData", order = 0)]
    public sealed class StatData : ScriptableObject
    {
        [field: SerializeField] public StatType Type { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public float MaxValue { get; private set; }

        private void OnValidate()
        {
            if (Value > MaxValue)
                Value = MaxValue;
        }
    }
}