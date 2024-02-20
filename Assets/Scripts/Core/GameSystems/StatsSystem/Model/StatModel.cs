using System;
using System.Collections.Generic;
using Core.GameSystems.StatsSystem.Enum;
using Core.GameSystems.StatsSystem.Modifiers;
using UnityEngine;

namespace Core.GameSystems.StatsSystem.Model
{
    public sealed class StatModel : IStatModel
    {
        private float _baseValue;
        private List<IStatModifier> _modifiers;
        private List<IStatModifier> _tickableModifiers;
        
        public StatType Type { get; }
        public float Value => CalculateValue();

        public event Action OnValueChanged;

        public StatModel(StatType type, float baseValue)
        {
            Type = type;
            _baseValue = baseValue;
            _modifiers = new List<IStatModifier>();
            _tickableModifiers = new List<IStatModifier>();
        }

        public void ApplyPermanentModifier(IStatModifier modifier)
        {
            _baseValue = modifier.Apply(_baseValue);
        }

        public void AddModifier(IStatModifier modifier, bool isTickable)
        {
            if (isTickable)
                _tickableModifiers.Add(modifier);
            else
                _modifiers.Add(modifier);
        }

        public void RemoveModifier(IStatModifier modifier, bool isTickable)
        {
            if (isTickable)
            {
                if (_modifiers.Contains(modifier))
                    _modifiers.Remove(modifier);
                else
                    Debug.LogError($"Modifier {modifier} not exist");
            }
            else
            {
                if (_modifiers.Contains(modifier))
                    _modifiers.Remove(modifier);
                else
                    Debug.LogError($"Modifier {modifier} not exist");
            }
        }
        
        public void Tick()
        {
            foreach (var modifier in _tickableModifiers)
                ApplyPermanentModifier(modifier);
        }

        private float CalculateValue()
        {
            var value = _baseValue;

            foreach (var modifier in _modifiers)
                value = modifier.Apply(value);
            
            return (float)Math.Round(value, 4);
        }
    }
}