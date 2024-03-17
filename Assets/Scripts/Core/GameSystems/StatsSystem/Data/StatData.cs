using Core.GameSystems.StatsSystem.Enum;
using UnityEngine;

namespace Core.GameSystems.StatsSystem.Data
{
    [CreateAssetMenu(fileName = "StatData", menuName = "StatsSystem/StatData", order = 0)]
    public sealed class StatData : ScriptableObject
    {
        [field: SerializeField] public StatId Id { get; private set; }
        [field: SerializeField] public float BaseValue { get; private set; }
    }
}