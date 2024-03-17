using Core.GameSystems.AbilitySystem;
using Core.GameSystems.AbilitySystem.Enums;
using Core.GameSystems.AbilitySystem.Factory;
using Core.GameSystems.AbilitySystem.Model;
using Core.GameSystems.InventorySystem;
using Core.GameSystems.StatsSystem;
using Core.GameSystems.UpgradingSystem;
using Zenject;

namespace Core.Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallSystems();
        }

        private void InstallSystems()
        {
            Container.Bind<CharacterInventorySystem>().AsSingle().Lazy();
            Container.Bind(typeof(ITickable), typeof(CharacterStatsSystem)).To<CharacterStatsSystem>().AsSingle();
            
            Container.BindFactory<AbilityType, IAbilityModel, AbilityModelsFactory>().FromFactory<CharacterAbilityModelsFactory>();
            Container.Bind<CharacterAbilitySystem>().AsSingle().Lazy();
            Container.Bind(typeof(MainCharacterUpgradingSystem), typeof(IInitializable)).To<MainCharacterUpgradingSystem>().AsSingle().NonLazy();
        } 
    }
}