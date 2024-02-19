using System.Threading.Tasks;
using Game.CameraController.StatesMachine;
using Game.CameraController.StatesMachine.Enums;
using UnityEngine;

namespace Game.MainCharacter.StatesMachine.States
{
    public class ThrowingState : MainCharacterStateAbstract
    {
        [SerializeField] private CharacterCameraStateMachine _cameraStateMachine;
        
        public override void ResetState()
        {
            
        }

        public override async Task Enter()
        {
            // await _cameraStateMachine.SetState(CharacterCameraControllerState.Throwing);
        }

        public override async Task Exit()
        {
            
        }
    }
}