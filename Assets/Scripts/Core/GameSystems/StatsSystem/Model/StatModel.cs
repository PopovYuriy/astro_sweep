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
        public float MaxValue { get; }

        public event Action OnValueChanged;

        public StatModel(StatType type, float baseValue, float maxValue)
        {
            Type = type;
            _baseValue = baseValue;
            MaxValue = maxValue;
            _modifiers = new List<IStatModifier>();
            _tickableModifiers = new List<IStatModifier>();
        }

        public void ApplyPermanentModifier(IStatModifier modifier)
        {
            var previousValue = _baseValue;
            
            _baseValue = Mathf.Clamp(modifier.Apply(_baseValue), 0f, MaxValue);
            
            if (Math.Abs(previousValue - Value) > float.Epsilon)
                OnValueChanged?.Invoke();
        }

        public void AddModifier(IStatModifier modifier)
        {
            var previousValue = Value;
            
            if (modifier.IsTimeBased)
                _tickableModifiers.Add(modifier);
            else
                _modifiers.Add(modifier);
            
            if (Math.Abs(previousValue - Value) > float.Epsilon)
                OnValueChanged?.Invoke();
        }

        public void RemoveModifier(IStatModifier modifier)
        {
            if (modifier.IsTimeBased)
            {
                if (_tickableModifiers.Contains(modifier))
                    _tickableModifiers.Remove(modifier);
                else
                    Debug.LogError($"Modifier {modifier} not exist");
            }
            else
            {
                var previousValue = Value;
                if (_modifiers.Contains(modifier))
                    _modifiers.Remove(modifier);
                else
                {
                    Debug.LogError($"Modifier {modifier} not exist");
                    return;
                }

                if (Math.Abs(previousValue - Value) > float.Epsilon)
                    OnValueChanged?.Invoke();
            }
        }
        
        public void Tick()
        {
            var modifiers = _tickableModifiers.ToArray();
            foreach (var modifier in modifiers)
                ApplyPermanentModifier(modifier);
        }

        private float CalculateValue()
        {
            var value = _baseValue;

            foreach (var modifier in _modifiers)
                value = modifier.Apply(value);

            value = (float) Math.Round(value, 4);
            return Mathf.Clamp(value, 0f, MaxValue);;
        }
    }
}