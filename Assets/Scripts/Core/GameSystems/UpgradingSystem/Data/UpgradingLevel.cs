using System;
using Core.GameSystems.UpgradingSystem.Data.Recipe;
using UnityEngine;

namespace Core.GameSystems.UpgradingSystem.Data
{
    [Serializable]
    public sealed record UpgradingLevel
    {
        [field: SerializeField] public UpgradingRecipe Recipe { get; private set; }
        [field: SerializeField] public UpgradingLevelData[] NewValues { get; private set; }
    }
}