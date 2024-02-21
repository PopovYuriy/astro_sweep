using System;
using System.Linq;
using System.Threading.Tasks;
using Core.GameSystems.AbilitySystem;
using Core.GameSystems.AbilitySystem.Enums;
using Core.GameSystems.AbilitySystem.Model;
using Game.MainCharacter.Abilities.Runners;
using Game.MainCharacter.StatesMachine;
using Game.MainCharacter.StatesMachine.Enums;
using Tools.CSharp;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities
{
    public class AbilityRunController : MonoBehaviour
    {
        [SerializeField] private MainCharacterStateMachine _characterStateMachine;
        [SerializeField] private TypeToRunnerMap[] _runnersMap;

        private CharacterAbilitySystem _abilitySystem;

        private TypeToRunnerMap _currentRunner;
        private IAbilityModel _currentAbilityModel;

        [Inject]
        private void Construct(CharacterAbilitySystem abilitySystem)
        {
            _abilitySystem = abilitySystem;
            foreach (var runnerMap in _runnersMap)
            {
                var model = _abilitySystem.GetAbilityModel(runnerMap.Type);
                runnerMap.Runner.SetData(model.Data);
            }
        }

        private void OnDestroy()
        {
            if (_currentRunner != null)
                _currentRunner.Runner.OnStop -= RunnerStopHandler;

            if (_currentAbilityModel != null)
                _currentAbilityModel.OnReadyChanged -= AbilityIsReadyChangedHandler;
        }

        public void ProcessAbilityRunner(AbilityType type)
        {
            if (_currentRunner == null)
            {
                var abilityModel = _abilitySystem.GetAbilityModel(type);
                if (!abilityModel.IsAvailable || !abilityModel.IsReady)
                    return;
                
                var runner = _runnersMap.FirstOrDefault(map => map.Type == type)?.Runner;
                if (runner == null)
                {
                    Debug.LogWarning($"Here is no binded runner for ability type {type}");
                    return;
                }

                RunAbilityAsync(type, runner, abilityModel).Run();
            }
            else
            {
                _currentRunner.Runner.Stop();
            }
        }

        private async Task RunAbilityAsync(AbilityType type, AbilityRunnerAbstract runner, IAbilityModel model)
        {
            var characterState = DetermineCharacterState(type);
            await _characterStateMachine.SetState(characterState);
            
            _currentAbilityModel = model;
            _currentAbilityModel.OnReadyChanged += AbilityIsReadyChangedHandler;
            
            runner.Run();
            runner.OnStop += RunnerStopHandler;
            _currentRunner = new TypeToRunnerMap(type, runner);
        }

        private void RunnerStopHandler()
        {
            UnsubscribeCurrentAbility();
        }
        
        private void AbilityIsReadyChangedHandler()
        {
            if (_currentAbilityModel.IsReady)
                return;
            
            var runner = _currentRunner.Runner;
            UnsubscribeCurrentAbility();
            runner.Stop();
        }

        private void UnsubscribeCurrentAbility()
        {
            _currentRunner.Runner.OnStop -= RunnerStopHandler;
            _currentRunner = null;
            _characterStateMachine.SetState(MainCharacterState.Idle).Run();

            _currentAbilityModel.OnReadyChanged -= AbilityIsReadyChangedHandler;
            _currentAbilityModel = null;
        }

        private MainCharacterState DetermineCharacterState(AbilityType abilityType)
        {
            switch (abilityType)
            {
                case AbilityType.Vacuuming:
                    return MainCharacterState.Vacuuming;
                case AbilityType.Throwing:
                    return MainCharacterState.Throwing;
                default:
                    Debug.LogWarning($"There is no state for ability {abilityType}");
                    return MainCharacterState.Idle;
            }
        }
        
        [Serializable]
        private sealed class TypeToRunnerMap
        {
            [field: SerializeField] public AbilityType Type { get; private set; }
            [field: SerializeField] public AbilityRunnerAbstract Runner { get; private set; }

            public TypeToRunnerMap(AbilityType type, AbilityRunnerAbstract runner)
            {
                Type = type;
                Runner = runner;
            }
        }
    }
}