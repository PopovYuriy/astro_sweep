using System;
using System.Collections.Generic;
using System.Linq;
using Core.GameSystems.InventorySystem;
using Core.GameSystems.InventorySystem.Enums;
using Core.GameSystems.InventorySystem.Model;
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
        [SerializeField] [Space] private Rect _stateLabelRect;
        [SerializeField] [Space] private Rect _chargeLabelRect;
        [SerializeField] [Space] private Rect _inventoryLabelRect;
        [SerializeField] [Space] private Rect _inventoryContainerInitialRect;
        [SerializeField] private float _inventorySpacing;
        [SerializeField] private GUIStyle _textStyle;

        private Dictionary<string, InventoryContainerModel> _inventoryModelsMap;
        private CharacterStatsSystem _statsSystem;
        private IStatModel _chargeStat;
        private bool _initialized;

        [Inject]
        private void Construct(CharacterStatsSystem statsSystem, CharacterInventorySystem inventorySystem)
        {
            _statsSystem = statsSystem;
            var names = Enum.GetNames(typeof(ItemCollectionType));
            _inventoryModelsMap = names.Select(name => 
                    new KeyValuePair<string, InventoryContainerModel>
                        (name, inventorySystem.GetInventoryContainer((ItemCollectionType) Enum.Parse(typeof(ItemCollectionType), name))))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            
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
            GUI.Label(_inventoryLabelRect, "Inventory : ", _textStyle);
            var inventoryIndex = 0;
            foreach (var collectionType in _inventoryModelsMap.Keys)
            {
                var rect = _inventoryContainerInitialRect;
                rect.y += _inventorySpacing * inventoryIndex++;
                GUI.Label(rect, $"{collectionType} : {_inventoryModelsMap[collectionType].Count}/{_inventoryModelsMap[collectionType].Capacity}", _textStyle);
            }
            
            GUI.EndGroup();
        }
    }
}