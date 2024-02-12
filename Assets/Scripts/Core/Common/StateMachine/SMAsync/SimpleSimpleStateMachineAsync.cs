using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.StateMachine.Api;
using Tools.CSharp;

namespace Core.Common.StateMachine.SMAsync
{
    public class SimpleSimpleStateMachineAsync<T> : ISimpleStateMachineAsync<T>
    {
        private Dictionary<T, IStateAsync> _states;
        private IStateAsync _currentState;

        public SimpleSimpleStateMachineAsync()
        {
            _states = new Dictionary<T, IStateAsync>();
        }

        public void RegisterState(T stateKey, IStateAsync state)
        {
            if (_states.ContainsKey(stateKey))
                throw new Exception($"State {stateKey} is already registered");
            
            _states.Add(stateKey, state);
            state.ResetState();
        }

        public void SetState(T stateKey)
        {
            if (!_states.ContainsKey(stateKey))
                throw new Exception($"State {stateKey} is not registered");
            
            SetStateAsync(stateKey).Run();
        }

        private async Task SetStateAsync(T stateKey)
        {
            if (_currentState != null)
            {
                await _currentState.Exit();
                _currentState.ResetState();
            }

            _currentState = _states[stateKey];
            await _currentState.Enter();
        }
    }
}