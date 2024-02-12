using Core.GameSystems.InventorySystem;
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
            Container.Bind<InventorySystem>().AsSingle().Lazy();
        } 
    }
}