using System;
using Core.GameSystems.AbilitySystem.Data;
using UnityEngine;

namespace Core.GameSystems.AbilitySystem.Model
{
    public sealed class MovingAbilityModel : IAbilityModel
    {
        private bool _isInitialized;
        
        public AbilityData Data { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsReady { get; private set; }

        public Vector2 MoveDirection { get; set; }

        public event Action<IAbilityModel> OnReadyChanged;

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