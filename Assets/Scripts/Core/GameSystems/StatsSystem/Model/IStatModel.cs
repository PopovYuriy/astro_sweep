using System;
using Core.GameSystems.StatsSystem.Enum;
using Core.GameSystems.StatsSystem.Modifiers;
using Zenject;

namespace Core.GameSystems.StatsSystem.Model
{
    public interface IStatModel : ITickable
    {
        event Action OnValueChanged;
        
        StatType Type { get; }
        float Value { get; }
        float MaxValue { get; }

        void ApplyPermanentModifier(IStatModifier modifier);
        void AddModifier(IStatModifier modifier);
        void RemoveModifier(IStatModifier modifier);
    }
}