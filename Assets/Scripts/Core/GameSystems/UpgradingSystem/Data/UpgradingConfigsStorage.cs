using UnityEngine;

namespace Core.GameSystems.UpgradingSystem.Data
{
    [CreateAssetMenu(fileName = "UpgradingConfig", menuName = "UpgradingSystem/UpgradingConfig", order = 0)]
    public sealed class UpgradingConfigsStorage : ScriptableObject
    {
        [field: SerializeField] public UpgradingStatConfig[] Items { get; private set; }
    }
}