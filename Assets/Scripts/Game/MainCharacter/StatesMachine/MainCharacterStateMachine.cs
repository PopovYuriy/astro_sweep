using System;
using System.Threading.Tasks;
using Core.Common.StateMachine.SimpleSMAsync;
using Game.MainCharacter.StatesMachine.Enums;
using Game.MainCharacter.StatesMachine.States;
using Tools.CSharp;
using UnityEngine;

namespace Game.MainCharacter.StatesMachine
{
    public sealed class MainCharacterStateMachine : MonoBehaviour
    {
        [SerializeField] private StateMap[] _states;

        private SimpleStateMachineAsync<MainCharacterState> _stateMachine;

        public MainCharacterState CurrentState => _stateMachine.CurrentState;

        private void Start()
        {
            _stateMachine = new SimpleStateMachineAsync<MainCharacterState>();

            foreach (var stateMap in _states)
                _stateMachine.RegisterState(stateMap.StateKey, stateMap.State);
            
            _stateMachine.SetState(MainCharacterState.Idle).Run();
        }

        public async Task SetState(MainCharacterState state)
        {
            await _stateMachine.SetState(state);
        }

        [Serializable]
        private sealed class StateMap
        {
            [field: SerializeField] public MainCharacterState StateKey { get; private set; }
            [field: SerializeField] public MainCharacterStateAbstract State { get; private set; }
        } 
    }
}