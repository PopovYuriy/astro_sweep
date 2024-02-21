namespace Core.GameSystems.StatsSystem.Modifiers
{
    public abstract class BaseModifierAbstract : IStatModifier
    {
        public bool IsTimeBased { get; protected set; }
        protected float Value { get; }

        protected BaseModifierAbstract(float value)
        {
            Value = value;
        }

        public abstract float Apply(float value);
    }
}