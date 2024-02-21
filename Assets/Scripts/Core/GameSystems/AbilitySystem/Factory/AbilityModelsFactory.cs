using System;
using Core.GameSystems.AbilitySystem.Data;
using Core.GameSystems.AbilitySystem.Enums;
using Core.GameSystems.AbilitySystem.Model;
using Zenject;

namespace Core.GameSystems.AbilitySystem.Factory
{
    public sealed class CharacterAbilityModelsFactory : IFactory<AbilityType, IAbilityModel>
    {
        private DiContainer _container;
        private AbilitiesConfig _abilitiesConfig;

        [Inject]
        private void Construct(DiContainer container, AbilitiesConfig abilitiesConfig)
        {
            _container = container;
            _abilitiesConfig = abilitiesConfig;
        }
        
        public IAbilityModel Create(AbilityType abilityType)
        {
            switch (abilityType)
            {
                case AbilityType.Vacuuming:
                    return InitializeModel(abilityType, new VacuumingAbilityModel());
                case AbilityType.Throwing:
                    return InitializeModel(abilityType, new ThrowingAbilityModel());
                case AbilityType.Charging:
                    return InitializeModel(abilityType, new ChargingAbilityModel());
                default:
                    throw new ArgumentException($"Can't create ability model by type {abilityType}");
            }
        }

        private IAbilityModel InitializeModel(AbilityType abilityType, IAbilityModel model)
        {
            _container.Inject(model);
            model.Initialize(_abilitiesConfig.GetAbilityData(abilityType));
            return model;
        }
    }

    public sealed class AbilityModelsFactory : PlaceholderFactory<AbilityType, IAbilityModel> { }
}