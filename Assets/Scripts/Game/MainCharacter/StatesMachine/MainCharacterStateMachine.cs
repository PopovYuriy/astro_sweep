using System;
using Core.Common.StateMachine.SMAsync;
using Game.MainCharacter.StatesMachine.Enums;
using Game.MainCharacter.StatesMachine.States;
using UnityEngine;

namespace Game.MainCharacter.StatesMachine
{
    public sealed class MainCharacterStateMachine : MonoBehaviour
    {
        [SerializeField] private StateMap[] _states;

        private SimpleStateMachineAsync<MainCharacterState> _stateMachine;

        private void Start()
        {
            _stateMachine = new SimpleStateMachineAsync<MainCharacterState>();

            foreach (var stateMap in _states)
                _stateMachine.RegisterState(stateMap.StateKey, stateMap.State);
            
            _stateMachine.SetState(MainCharacterState.Idle);
        }

        public void SetState(MainCharacterState state)
        {
            _stateMachine.SetState(state);
        }

        [Serializable]
        private sealed class StateMap
        {
            [field: SerializeField] public MainCharacterState StateKey { get; private set; }
            [field: SerializeField] public MainCharacterStateAbstract State { get; private set; }
        } 
    }
}