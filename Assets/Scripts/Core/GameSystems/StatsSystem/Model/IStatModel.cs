using System;
using Core.GameSystems.StatsSystem.Enum;
using Core.GameSystems.StatsSystem.Modifiers;
using Zenject;

namespace Core.GameSystems.StatsSystem.Model
{
    public interface IStatModel : ITickable
    {
        event Action OnValueChanged;
        
        StatId Id { get; }
        float Value { get; }

        void UpdateBaseValue(float value);
        void ApplyPermanentModifier(IStatModifier modifier);
        void AddModifier(IStatModifier modifier);
        void RemoveModifier(IStatModifier modifier);
    }
}