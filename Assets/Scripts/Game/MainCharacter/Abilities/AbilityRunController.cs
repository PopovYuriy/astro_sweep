using System;
using System.Linq;
using System.Threading.Tasks;
using Core.GameSystems.AbilitySystem;
using Core.GameSystems.AbilitySystem.Enums;
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

        [Inject]
        private void Construct(CharacterAbilitySystem abilitySystem)
        {
            _abilitySystem = abilitySystem;
        }

        private void OnDestroy()
        {
            if (_currentRunner != null)
                _currentRunner.Runner.OnStopped -= RunnerStoppedHandler;
        }

        public void TryRunAbility(AbilityType type)
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

            RunAbilityAsync(type, runner).Run();
        }

        private async Task RunAbilityAsync(AbilityType type, AbilityRunnerAbstract runner)
        {
            if (_currentRunner == null)
            {
                var characterState = DetermineCharacterState(type);
                await _characterStateMachine.SetState(characterState);
                runner.Run();
                runner.OnStopped += RunnerStoppedHandler;
                _currentRunner = new TypeToRunnerMap(type, runner);
            }
            else
            {
                _currentRunner.Runner.Stop();
            }
        }

        private void RunnerStoppedHandler()
        {
            _currentRunner.Runner.OnStopped -= RunnerStoppedHandler;
            _currentRunner = null;
            _characterStateMachine.SetState(MainCharacterState.Idle).Run();
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