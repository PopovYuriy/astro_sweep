using System;
using Core.GameSystems.StatsSystem.Enum;

namespace Core.GameSystems.StatsSystem.Modifiers
{
    public static class ModifierFactory
    {
        public static IStatModifier CreateModifier(ModifierType type, float value)
        {
            switch (type)
            {
                case ModifierType.OnceDecrease:
                    return new DecreaseModifier(value);
                case ModifierType.OnceIncrease:
                    return new IncreaseModifier(value);
                case ModifierType.DecreasePerSec:
                    return new DecreaseModifierByTime(value);
                case ModifierType.IncreasePerSec:
                    return new IncreaseModifierByTime(value);
                default:
                    throw new ArgumentException($"Unknown modifier type : {type}");
            }
        }
    }
}