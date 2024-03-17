using System;
using Core.GameSystems.InventorySystem.Enums;
using UnityEngine;

namespace Core.GameSystems.UpgradingSystem.Data.Recipe
{
    [Serializable]
    public sealed record RecipeIngredient
    {
        [field: SerializeField] public ItemType Type { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}