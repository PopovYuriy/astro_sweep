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
            IAbilityModel result = null;
            
            switch (abilityType)
            {
                case AbilityType.Vacuuming:
                    result = InitializeModel(abilityType, new VacuumingAbilityModel());
                    break;
                case AbilityType.Throwing:
                    result = InitializeModel(abilityType, new ThrowingAbilityModel());
                    break;
            }

            return result;
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