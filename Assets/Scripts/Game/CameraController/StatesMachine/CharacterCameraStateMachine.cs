using System;
using System.Threading.Tasks;
using Core.Common.StateMachine.SimpleSMAsync;
using Game.CameraController.StatesMachine.Enums;
using Game.CameraController.StatesMachine.States;
using Tools.CSharp;
using UnityEngine;

namespace Game.CameraController.StatesMachine
{
    public sealed class CharacterCameraStateMachine : MonoBehaviour
    {
        [SerializeField] private StateMap[] _states;

        private SimpleStateMachineAsync<CharacterCameraControllerState> _stateMachine;

        private void Awake()
        {
            _stateMachine = new SimpleStateMachineAsync<CharacterCameraControllerState>();

            foreach (var stateMap in _states)
                _stateMachine.RegisterState(stateMap.StateKey, stateMap.State);
            
            _stateMachine.SetState(CharacterCameraControllerState.ThirdPerson).Run();
        }

        public async Task SetState(CharacterCameraControllerState state)
        {
            await _stateMachine.SetState(state);
        }

        [Serializable]
        private sealed class StateMap
        {
            [field: SerializeField] public CharacterCameraControllerState StateKey { get; private set; }
            [field: SerializeField] public CharacterCameraStateAbstract State { get; private set; }
        } 
    }
}