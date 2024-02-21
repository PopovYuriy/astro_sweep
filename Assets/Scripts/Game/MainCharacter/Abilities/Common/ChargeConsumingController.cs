using Core.GameSystems.StatsSystem;
using Core.GameSystems.StatsSystem.Enum;
using Core.GameSystems.StatsSystem.Model;
using Core.GameSystems.StatsSystem.Modifiers;
using Game.MainCharacter.Abilities.Runners;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities.Common
{
    [RequireComponent(typeof(AbilityRunnerAbstract))]
    public sealed class ChargeConsumingController : MonoBehaviour
    {
        [SerializeField] private AbilityRunnerAbstract _abilityRunner;

        private CharacterStatsSystem _statsSystem;
        private IStatModel _chargeStatModel;
        private IStatModifier _chargeStatModifier;

        [Inject]
        private void Construct(CharacterStatsSystem statsSystem)
        {
            _statsSystem = statsSystem;
        }

        private void OnValidate()
        {
            _abilityRunner ??= GetComponent<AbilityRunnerAbstract>();
        }

        private void Start()
        {
            _chargeStatModel = _statsSystem.GetStatModel(StatType.Charge);
            var data = _abilityRunner.Data.ChargingData;
            _chargeStatModifier = ModifierFactory.CreateModifier(data.ModifierType, data.Value);
            
            _abilityRunner.OnAct += AbilityActHandler;
            _abilityRunner.OnStop += AbilityStopHandler;
        }

        private void OnDestroy()
        {
            _abilityRunner.OnAct -= AbilityActHandler;
            _abilityRunner.OnStop -= AbilityStopHandler;
        }

        private void AbilityActHandler()
        {
            if (_chargeStatModifier.IsTimeBased)
                _chargeStatModel.AddModifier(_chargeStatModifier);
            else
                _chargeStatModel.ApplyPermanentModifier(_chargeStatModifier);
        }

        private void AbilityStopHandler()
        {
            if (_chargeStatModifier.IsTimeBased)
                _chargeStatModel.RemoveModifier(_chargeStatModifier);
        }
    }
}