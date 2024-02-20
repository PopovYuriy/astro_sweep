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

        void ApplyPermanentModifier(IStatModifier modifier);
        void AddModifier(IStatModifier modifier, bool isTickable);
        void RemoveModifier(IStatModifier modifier, bool isTickable);
    }
}