using System.Linq;
using Core.GameSystems.AbilitySystem.Data;
using Core.GameSystems.AbilitySystem.Enums;
using Core.GameSystems.AbilitySystem.Factory;
using Core.GameSystems.AbilitySystem.Model;
using Zenject;

namespace Core.GameSystems.AbilitySystem
{
    public sealed class CharacterAbilitySystem
    {
        private AbilityModelsFactory _factory;
        private IAbilityModel[] _abilityModels;

        [Inject]
        private void Construct(AbilityModelsFactory factory, AbilitiesConfig config)
        {
            _factory = factory;
            var abilityTypes = config.GetAbilityTypes();
            _abilityModels = new IAbilityModel[abilityTypes.Length];
            for (var i = 0; i < abilityTypes.Length; i++)
                _abilityModels[i] = _factory.Create(abilityTypes[i]);
        }

        public IAbilityModel GetAbilityModel(AbilityType type)
        {
            return _abilityModels.First(m => m.Data.Type == type);
        }
    }
}