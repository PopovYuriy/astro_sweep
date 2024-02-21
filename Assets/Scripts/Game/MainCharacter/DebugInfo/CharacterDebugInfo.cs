using Core.GameSystems.StatsSystem;
using Core.GameSystems.StatsSystem.Enum;
using Core.GameSystems.StatsSystem.Model;
using Game.MainCharacter.StatesMachine;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.DebugInfo
{
    public sealed class CharacterDebugInfo : MonoBehaviour
    {
        [SerializeField] private MainCharacterStateMachine _characterStateMachine;

        [Header("View settings")]
        [SerializeField] private Rect _windowRect;
        [SerializeField] private Rect _stateLabelRect;
        [SerializeField] private Rect _chargeLabelRect;
        [SerializeField] private GUIStyle _textStyle;

        private CharacterStatsSystem _statsSystem;
        private IStatModel _chargeStat;
        private bool _initialized;

        [Inject]
        private void Construct(CharacterStatsSystem statsSystem)
        {
            _statsSystem = statsSystem;
            _chargeStat = _statsSystem.GetStatModel(StatType.Charge);
            _initialized = true;
        }
        
        private void OnGUI()
        {
            if (!_initialized)
                return;
            
            GUI.BeginGroup(_windowRect);
            
            GUI.Label(_stateLabelRect, $"State : {_characterStateMachine.CurrentState.ToString()}", _textStyle);
            GUI.Label(_chargeLabelRect, $"Charge : {_chargeStat.Value}", _textStyle);
            
            GUI.EndGroup();
        }
    }
}