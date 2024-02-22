using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.GameSystems.AbilitySystem;
using Core.GameSystems.AbilitySystem.Enums;
using Core.GameSystems.AbilitySystem.Model;
using Game.MainCharacter.Abilities.Runners;
using Game.MainCharacter.StatesMachine;
using Game.MainCharacter.StatesMachine.Enums;
using Tools.CSharp;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities
{
    public sealed class AbilityRunController : MonoBehaviour
    {
        [SerializeField] private MainCharacterStateMachine _characterStateMachine;
        [SerializeField] private TypeToRunnerMap[] _runnersMap;

        private CharacterAbilitySystem _abilitySystem;

        private List<RunnerData> _currentRunners;

        // private TypeToRunnerMap _currentRunner;
        // private IAbilityModel _currentAbilityModel;

        [Inject]
        private void Construct(CharacterAbilitySystem abilitySystem)
        {
            _abilitySystem = abilitySystem;
            foreach (var runnerMap in _runnersMap)
            {
                var model = _abilitySystem.GetAbilityModel(runnerMap.Type);
                runnerMap.Runner.SetData(model.Data);
            }

            _currentRunners = new List<RunnerData>();
        }

        private void OnDestroy()
        {
            if (_currentRunners.Count > 0)
            {
                foreach (var runnerData in _currentRunners)
                {
                    runnerData.Runner.OnStop -= RunnerStopHandler;
                    runnerData.Model.OnReadyChanged -= AbilityIsReadyChangedHandler;
                }
            }
        }

        public void ProcessAbilityRunner(AbilityType type)
        {
            if (TryStopActiveAbility(type))
                return;
            
            if (CheckIsAbilityBlocked(type))
                return;
            
            var abilityModel = _abilitySystem.GetAbilityModel(type);
            
            if (!abilityModel.IsAvailable || !abilityModel.IsReady)
                return;

            if (abilityModel.Data.Mode == AbilityMode.Single)
                StopAllActiveRunners();
            
            var runner = _runnersMap.FirstOrDefault(map => map.Type == type)?.Runner;
            if (runner == null)
            {
                Debug.LogWarning($"Here is no binded runner for ability type {type}");
                return;
            }

            RunAbilityAsync(type, runner, abilityModel).Run();
        }

        private async Task RunAbilityAsync(AbilityType type, AbilityRunnerAbstract runner, IAbilityModel model)
        {
            var characterState = DetermineCharacterState(type);
            await _characterStateMachine.SetState(characterState);
            
            model.OnReadyChanged += AbilityIsReadyChangedHandler;
            
            runner.Run();
            runner.OnStop += RunnerStopHandler;
            
            var runnerData = new RunnerData(runner, model);
            _currentRunners.Add(runnerData);
        }

        private bool CheckIsAbilityBlocked(AbilityType type)
        {
            if (_currentRunners.Count == 0)
                return false;

            return _currentRunners.Any(data => data.Model.Data.AbilitiesToBlock.GetCollection().Contains(type));
        }

        private void RunnerStopHandler(AbilityRunnerAbstract runner)
        {
            var runnerData = _currentRunners.First(data => data.Runner == runner);
            UnsubscribeAbility(runnerData);
        }
        
        private void AbilityIsReadyChangedHandler(IAbilityModel model)
        {
            if (model.IsReady)
                return;
            
            var runnerData = _currentRunners.First(data => data.Model == model);
            StopAndDeleteAbility(runnerData);
        }

        private void StopAllActiveRunners()
        {
            foreach (var runnerData in _currentRunners)
                UnsubscribeAndStopAbility(runnerData);
            
            _currentRunners.Clear();
            _characterStateMachine.SetState(MainCharacterState.Idle).Run();
        }

        private bool TryStopActiveAbility(AbilityType type)
        {
            if (_currentRunners.Count == 0)
                return false;

            var runnerData = _currentRunners.FirstOrDefault(data => data.Model.Data.Type == type);
            if (runnerData == null)
                return false;

            StopAndDeleteAbility(runnerData);

            return true;
        }

        private void StopAndDeleteAbility(RunnerData data)
        {
            UnsubscribeAndStopAbility(data);

            _currentRunners.Remove(data);
            
            if (_currentRunners.Count == 0)
                _characterStateMachine.SetState(MainCharacterState.Idle).Run();
        }

        private void UnsubscribeAndStopAbility(RunnerData data)
        {
            UnsubscribeAbility(data);
            data.Runner.Stop();
        }

        private void UnsubscribeAbility(RunnerData data)
        {
            data.Runner.OnStop -= RunnerStopHandler;
            data.Model.OnReadyChanged -= AbilityIsReadyChangedHandler;
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

        private sealed class RunnerData
        {
            public AbilityRunnerAbstract Runner { get; }
            public IAbilityModel Model { get; }

            public RunnerData(AbilityRunnerAbstract runner, IAbilityModel model)
            {
                Runner = runner;
                Model = model;
            }
        }
    }
}