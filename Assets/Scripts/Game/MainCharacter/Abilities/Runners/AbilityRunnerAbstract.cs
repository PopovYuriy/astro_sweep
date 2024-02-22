using System;
using Core.GameSystems.AbilitySystem.Data;
using UnityEngine;

namespace Game.MainCharacter.Abilities.Runners
{
    public abstract class AbilityRunnerAbstract : MonoBehaviour
    {
        public AbilityData Data { get; private set; }

        public event Action OnAct;
        public event Action OnRun;
        public event Action<AbilityRunnerAbstract> OnStop;

        public void SetData(AbilityData data)
        {
            Data = data;
        }
        
        public void Run()
        {
            RunInternal();
            OnRun?.Invoke();
        }

        public void Stop()
        {
            StopInternal();
            OnStop?.Invoke(this);
        }

        protected abstract void RunInternal();
        protected abstract void StopInternal();

        protected void DispatchActed()
        {
            OnAct?.Invoke();
        }
    }
}