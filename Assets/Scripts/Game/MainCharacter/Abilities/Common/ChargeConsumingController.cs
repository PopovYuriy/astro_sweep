using Core.GameSystems.AbilitySystem.Model;
using Core.GameSystems.StatsSystem;
using Core.GameSystems.StatsSystem.Enum;
using Core.GameSystems.StatsSystem.Model;
using Core.GameSystems.StatsSystem.Modifiers;
using Game.MainCharacter.Abilities.Runners;
using Tools.Unity.SerializeInterface;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities.Common
{
    [RequireComponent(typeof(AbilityRunnerAbstract<>))]
    public sealed class ChargeConsumingController : MonoBehaviour
    {
        [SerializeField, SerializeInterface(typeof(IAbilityRunner))] private Component _abilityRunnerComponent;

        private CharacterStatsSystem _statsSystem;
        private IStatModel _chargeStatModel;
        private IStatModifier _chargeStatModifier;

        private IAbilityRunner _abilityRunner;

        [Inject]
        private void Construct(CharacterStatsSystem statsSystem)
        {
            _statsSystem = statsSystem;
        }

        private void Start()
        {
            _abilityRunner = _abilityRunnerComponent as IAbilityRunner;
            
            _chargeStatModel = _statsSystem.GetStatModel(StatType.Charge);
            var data = _abilityRunner.Model.Data.ChargingData;
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

        private void AbilityStopHandler(IAbilityRunner runner)
        {
            if (_chargeStatModifier.IsTimeBased)
                _chargeStatModel.RemoveModifier(_chargeStatModifier);
        }
    }
}