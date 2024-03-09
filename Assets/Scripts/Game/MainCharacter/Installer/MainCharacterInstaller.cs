using Game.MainCharacter.Input;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Installer
{
    public sealed class MainCharacterInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInputController _inputController;

        public override void InstallBindings()
        {
            Container.Bind<PlayerInputController>().FromInstance(_inputController);
        }
    }
}