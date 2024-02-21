namespace Core.GameSystems.StatsSystem.Modifiers
{
    public interface IStatModifier
    {
        bool IsTimeBased { get; }
        
        float Apply(float value);
    }
}