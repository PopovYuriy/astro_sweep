using UnityEngine;

namespace Game.MainCharacter.Abilities
{
    public abstract class AbilityRunnerAbstract : MonoBehaviour
    {
        public abstract void Run();
        public abstract void Stop();
    }
}