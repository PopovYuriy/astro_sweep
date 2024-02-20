namespace Core.GameSystems.StatsSystem.Modifiers
{
    public sealed class IncreaseModifier : BaseModifierAbstract
    {
        public IncreaseModifier(float value) : base(value) { }

        public override float Apply(float value)
        {
            return value + Value;
        }
    }
}