using System.Threading.Tasks;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.CameraController.StatesMachine.States
{
    public sealed class FirstPersonCameraState : CharacterCameraStateAbstract
    {
        [SerializeField] private CinemachineBrain _cinemachineBrain;
        [SerializeField] private CinemachineVirtualCameraBase _camera;
        
        public override void ResetState()
        {
            _camera.gameObject.SetActive(false);
        }

        public override async Task Enter()
        {
            _camera.gameObject.SetActive(true);
            await UniTask.NextFrame();
            await UniTask.WaitWhile(() => _cinemachineBrain.IsBlending);
        }

        public override async Task Exit()
        {
            _camera.gameObject.SetActive(false);
        }
    }
}