using System;
using Core.GameSystems.AbilitySystem.Data;
using Core.GameSystems.StatsSystem;
using Core.GameSystems.StatsSystem.Enum;
using Core.GameSystems.StatsSystem.Model;
using Zenject;

namespace Core.GameSystems.AbilitySystem.Model
{
    public sealed class ChargingAbilityModel : IAbilityModel
    {
        private bool _isInitialized;
        private IStatModel _chargeStatModel;
        
        public AbilityData Data { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsReady { get; private set; }
        
        public event Action OnReadyChanged;

        [Inject]
        private void Construct(CharacterStatsSystem statsSystem)
        {
            _chargeStatModel = statsSystem.GetStatModel(StatType.Charge);
        }
        
        public void Initialize(AbilityData data)
        {
            if (_isInitialized)
                return;
            
            _isInitialized = true;
            
            Data = data;
            IsAvailable = true;
            IsReady = _chargeStatModel.Value < _chargeStatModel.MaxValue;
            _chargeStatModel.OnValueChanged += ChargeValueChanged;
        }
        
        public void Dispose()
        {
            if (!_isInitialized)
                return;
            
            _chargeStatModel.OnValueChanged -= ChargeValueChanged;
        }

        private void ChargeValueChanged()
        {
            var isReadyPrevious = IsReady;
            IsReady = _chargeStatModel.Value < _chargeStatModel.MaxValue;
            
            if (IsReady != isReadyPrevious)
                OnReadyChanged?.Invoke();
        }
    }
}