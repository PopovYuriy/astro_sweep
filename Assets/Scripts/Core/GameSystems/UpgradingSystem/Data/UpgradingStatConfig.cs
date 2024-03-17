using Core.GameSystems.UpgradingSystem.Enums;
using UnityEngine;

namespace Core.GameSystems.UpgradingSystem.Data
{
    [CreateAssetMenu(fileName = "UpgradableStatConfig", menuName = "UpgradingSystem/UpgradableStatConfig", order = 0)]
    public sealed class UpgradingStatConfig : ScriptableObject
    {
        [field: SerializeField] public UpgradableItemId Id { get; private set; }
        [field: SerializeField] public UpgradingLevel[] Levels { get; private set; }
    } 
}