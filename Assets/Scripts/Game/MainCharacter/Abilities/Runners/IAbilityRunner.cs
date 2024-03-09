using System;
using Core.GameSystems.AbilitySystem.Model;
using Game.MainCharacter.StatesMachine.Enums;

namespace Game.MainCharacter.Abilities.Runners
{
    public interface IAbilityRunner
    {
        IAbilityModel Model { get; }
        MainCharacterState State { get; }

        event Action OnAct;
        event Action OnRun;
        event Action<IAbilityRunner> OnStop;

        void SetModel(IAbilityModel model);
        void Run();
        void Stop();
    }
}