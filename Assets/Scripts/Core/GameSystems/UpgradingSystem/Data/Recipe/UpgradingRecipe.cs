using System;
using UnityEngine;

namespace Core.GameSystems.UpgradingSystem.Data.Recipe
{
    [Serializable]
    public sealed record UpgradingRecipe
    {
        [field: SerializeField] public RecipeIngredient[] Ingredients { get; private set; }
    }
}