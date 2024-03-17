using System;
using Core.GameSystems.AbilitySystem.Data;
using Core.GameSystems.StatsSystem;
using Core.GameSystems.StatsSystem.Enum;
using Core.GameSystems.StatsSystem.Model;
using UnityEngine;
using Zenject;

namespace Core.GameSystems.AbilitySystem.Model
{
    public sealed class MovingAbilityModel : IAbilityModel
    {
        private bool _isInitialized;
        private IStatModel _moveSpeedStat;
        private IStatModel _rotationSpeedStat;

        public AbilityData Data { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsReady { get; private set; }

        public Vector2 MoveDirection { get; set; }
        public float MoveSpeed => _moveSpeedStat.Value;
        public float RotationSpeed => _rotationSpeedStat.Value;

        public event Action<IAbilityModel> OnReadyChanged;

        [Inject]
        private void Construct(CharacterStatsSystem statsSystem)
        {
            _moveSpeedStat = statsSystem.GetStatModel(StatId.MoveSpeed);
            _rotationSpeedStat = statsSystem.GetStatModel(StatId.RotationSpeed);
        }

        public void Initialize(AbilityData data)
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
            
            Data = data;
            IsAvailable = true;
            DetermineIsReady(false);
        }

        public void Dispose()
        {
            
        }

        private void DetermineIsReady(bool needDispatchEvent)
        {
            IsReady = true;
        }
    }
}