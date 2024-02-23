using Core.GameSystems.AbilitySystem.Model;

namespace Game.MainCharacter.Abilities.Runners
{
    public sealed class ChargingAbilityRunner : AbilityRunnerAbstract<ChargingAbilityModel>
    {
        protected override void RunInternal()
        {
            DispatchActed();
        }

        protected override void StopInternal() { }
    }
}