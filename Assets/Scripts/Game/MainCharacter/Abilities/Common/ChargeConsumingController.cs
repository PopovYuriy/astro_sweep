using Core.GameSystems.AbilitySystem.Data;
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
        private IStatModel _chargingEfficiencyStatModel;
        private IStatModel _consumingEfficiencyStatModel;
        private IStatModifier _chargeStatModifier;

        private ChargingData _data;

        private IAbilityRunner _abilityRunner;

        [Inject]
        private void Construct(CharacterStatsSystem statsSystem)
        {
            _statsSystem = statsSystem;
        }

        private void Start()
        {
            _abilityRunner = _abilityRunnerComponent as IAbilityRunner;
            
            _chargeStatModel = _statsSystem.GetStatModel(StatId.BatteryCharge);
            _chargingEfficiencyStatModel = _statsSystem.GetStatModel(StatId.BatteryChargingEfficiency);
            _consumingEfficiencyStatModel = _statsSystem.GetStatModel(StatId.BatteryConsumingEfficiency);
            _data = _abilityRunner.Model.Data.ChargingData;

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
            _chargeStatModifier = ModifierFactory.CreateModifier(_data.ModifierType, DetermineModifierValue());
            
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

        private float DetermineModifierValue()
        {
            float factor;
            if (_data.ModifierType == ModifierType.DecreasePerSec || _data.ModifierType == ModifierType.OnceDecrease)
                factor = _consumingEfficiencyStatModel.Value;
            else
                factor = _chargingEfficiencyStatModel.Value;

            return factor * _data.Value;
        }
    }
}