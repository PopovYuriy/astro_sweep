using System.Linq;
using Core.GameSystems.AbilitySystem.Enums;
using UnityEngine;

namespace Core.GameSystems.AbilitySystem.Data
{
    [CreateAssetMenu(fileName = "AbilitiesConfig", menuName = "Abilities/AbilitiesConfig", order = 0)]
    public sealed class AbilitiesConfig : ScriptableObject
    {
        [SerializeField] private AbilityData[] _abilitiesData;

        public AbilityData GetAbilityData(AbilityType type)
        {
            return _abilitiesData.First(d => d.Type == type);
        }
        
        public AbilityType[] GetAbilityTypes() => _abilitiesData.Select(d => d.Type).ToArray();
    }
}