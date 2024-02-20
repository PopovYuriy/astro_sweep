namespace Core.GameSystems.StatsSystem.Modifiers
{
    public abstract class BaseModifierAbstract : IStatModifier
    {
        protected float Value { get; }

        protected BaseModifierAbstract(float value)
        {
            Value = value;
        }

        public abstract float Apply(float value);
    }
}