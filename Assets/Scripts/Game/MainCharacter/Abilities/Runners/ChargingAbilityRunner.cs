namespace Game.MainCharacter.Abilities.Runners
{
    public sealed class ChargingAbilityRunner : AbilityRunnerAbstract
    {
        protected override void RunInternal()
        {
            DispatchActed();
        }

        protected override void StopInternal() { }
    }
}