using System;
using Core.GameSystems.StatsSystem.Enum;
using UnityEngine;

namespace Core.GameSystems.UpgradingSystem.Data
{
    [Serializable]
    public sealed record UpgradingLevelData
    {
        [field: SerializeField] public StatId StatId { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
    }
}