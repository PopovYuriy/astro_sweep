using System.Threading.Tasks;
using Core.Common.StateMachine.Api;
using UnityEngine;

namespace Game.MainCharacter.StatesMachine.States
{
    public abstract class MainCharacterStateAbstract : MonoBehaviour, IStateAsync
    {
        public abstract void ResetState();
        public abstract Task Enter();
        public abstract Task Exit();
    }
}