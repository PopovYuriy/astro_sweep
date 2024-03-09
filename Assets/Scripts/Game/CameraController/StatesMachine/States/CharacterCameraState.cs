using System.Threading.Tasks;
using Cinemachine;
using Core.Common.StateMachine.Api;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.CameraController.StatesMachine.States
{
    public class CharacterCameraState : MonoBehaviour, IStateAsync
    {
        [SerializeField] private CinemachineBrain _cinemachineBrain;
        [field: SerializeField] public CinemachineVirtualCameraBase Camera { get; private set; }
        
        public void ResetState()
        {
            Camera.gameObject.SetActive(false);
        }

        public async Task Enter()
        {
            OnEnterBefore();
            
            Camera.gameObject.SetActive(true);
            await UniTask.NextFrame();
            await UniTask.WaitWhile(() => _cinemachineBrain.IsBlending);
            
            OnEnterAfter();
        }

        public async Task Exit()
        {
            OnExitBefore();
            
            Camera.gameObject.SetActive(false);
            
            OnExitAfter();
        }

        protected virtual void OnEnterBefore(){}
        protected virtual void OnEnterAfter(){}
        protected virtual void OnExitBefore(){}
        protected virtual void OnExitAfter(){}
    }
}