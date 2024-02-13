using System;
using System.Linq;
using Core.GameSystems.AbilitySystem;
using Core.GameSystems.AbilitySystem.Enums;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities
{
    public class AbilityRunController : MonoBehaviour
    {
        [SerializeField] private TypeToRunnerMap[] _runnersMap;

        private CharacterAbilitySystem _abilitySystem;

        private TypeToRunnerMap _currentRunner;

        public event Action<AbilityType> OnAbilityRan;
        public event Action OnAbilitiesStopped;

        [Inject]
        private void Construct(CharacterAbilitySystem abilitySystem)
        {
            _abilitySystem = abilitySystem;
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

            if (_currentRunner == null)
            {
                runner.Run();
                _currentRunner = new TypeToRunnerMap(type, runner);
                OnAbilityRan?.Invoke(type);
            }
            else
            {
                _currentRunner.Runner.Stop();
                if (_currentRunner.Type != type)
                {
                    runner.Run();
                    _currentRunner = new TypeToRunnerMap(type, runner);
                    OnAbilityRan?.Invoke(type);
                }
                else
                {
                    _currentRunner = null;
                    OnAbilitiesStopped?.Invoke();
                }
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