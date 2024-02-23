using System;
using Core.GameSystems.AbilitySystem.Model;
using UnityEngine;

namespace Game.MainCharacter.Abilities.Runners
{
    public abstract class AbilityRunnerAbstract<T> : MonoBehaviour, IAbilityRunner where T : IAbilityModel
    {
        public T Model { get; private set; }

        IAbilityModel IAbilityRunner.Model => Model;

        public event Action OnAct;
        public event Action OnRun;
        public event Action<IAbilityRunner> OnStop;

        public void SetModel(IAbilityModel model)
        {
            Model = (T) model;
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