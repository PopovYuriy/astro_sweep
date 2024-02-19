using System.Threading.Tasks;
using Core.Common.StateMachine.Api;
using UnityEngine;

namespace Game.CameraController.StatesMachine.States
{
    public abstract class CharacterCameraStateAbstract : MonoBehaviour, IStateAsync
    {
        public abstract void ResetState();
        public abstract Task Enter();
        public abstract Task Exit();
    }
}