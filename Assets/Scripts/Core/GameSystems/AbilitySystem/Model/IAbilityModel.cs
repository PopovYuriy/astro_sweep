using System;
using Core.GameSystems.AbilitySystem.Data;

namespace Core.GameSystems.AbilitySystem.Model
{
    public interface IAbilityModel : IDisposable
    {
        AbilityData Data { get; }
        bool IsAvailable { get; }
        bool IsReady { get; }

        event Action<IAbilityModel> OnReadyChanged;

        void Initialize(AbilityData data);
    }
}