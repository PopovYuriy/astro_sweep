using UnityEngine;

namespace Core.GameSystems.StatsSystem.Data
{
    [CreateAssetMenu(fileName = "StatsConfig", menuName = "StatsSystem/StatsConfig", order = 0)]
    public sealed class StatsConfig : ScriptableObject
    {
        [field: SerializeField] public StatData[] Stats { get; private set; }
    }
}