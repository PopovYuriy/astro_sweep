namespace Core.GameSystems.StatsSystem.Modifiers
{
    public sealed class DecreaseModifier : BaseModifierAbstract
    {
        public DecreaseModifier(float value) : base(value)
        {
            IsTimeBased = false;
        }

        public override float Apply(float value)
        {
            return value - Value;
        }
    }
}