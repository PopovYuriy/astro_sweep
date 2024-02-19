using System;
using UnityEngine;

namespace Game.MainCharacter.Abilities
{
    public abstract class AbilityRunnerAbstract : MonoBehaviour
    {
        public event Action OnStopped;
        public void Run()
        {
            RunInternal();
        }

        public void Stop()
        {
            StopInternal();
            OnStopped?.Invoke();
        }

        protected abstract void RunInternal();
        protected abstract void StopInternal();
    }
}