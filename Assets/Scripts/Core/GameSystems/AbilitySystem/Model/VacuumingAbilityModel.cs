using System;
using Core.GameSystems.AbilitySystem.Data;
using Core.GameSystems.StatsSystem;
using Core.GameSystems.StatsSystem.Enum;
using Core.GameSystems.StatsSystem.Model;
using Zenject;

namespace Core.GameSystems.AbilitySystem.Model
{
    public sealed class VacuumingAbilityModel : IAbilityModel
    {
        private bool _isInitialized;
        private IStatModel _chargeStatModel;
        
        public AbilityData Data { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsReady { get; private set; }

        public event Action<IAbilityModel> OnReadyChanged;

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
            DetermineIsReady(false);
            
            _chargeStatModel.OnValueChanged += ChargeValueChangedHandler;
        }

        public void Dispose()
        {
            _chargeStatModel.OnValueChanged -= ChargeValueChangedHandler;
        }
        
        private void ChargeValueChangedHandler()
        {
            DetermineIsReady(true);
        }

        private void DetermineIsReady(bool needDispatchEvent)
        {
            var previousValue = IsReady;
            IsReady = _chargeStatModel.Value >= Data.ChargingData.Value;
            
            if (needDispatchEvent && previousValue != IsReady)
                OnReadyChanged?.Invoke(this);
        }
    }
}