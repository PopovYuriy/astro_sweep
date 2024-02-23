namespace Core.GameSystems.AbilitySystem.Enums
{
    public enum AbilityType
    {
        Vacuuming = 1,
        Throwing = 1 << 2,
        Blowing = 1 << 3,
        Charging = 1 << 4,
        Moving = 1 << 5
    }
}