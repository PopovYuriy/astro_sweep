using System;
using Core.GameSystems.StatsSystem;
using Core.GameSystems.UpgradingSystem.Data;

namespace Core.GameSystems.UpgradingSystem.Items
{
    public sealed class UpgradableItem : IUpgradableItem
    {
        private CharacterStatsSystem _statsSystem;
        
        public int CurrentLevel { get; private set; }
        public UpgradingStatConfig Config { get; private set; }

        public void Initialize(int currentLevel, CharacterStatsSystem statsSystem, UpgradingStatConfig upgradingConfig)
        {
            CurrentLevel = currentLevel;
            Config = upgradingConfig;
            _statsSystem = statsSystem;

            if (CurrentLevel == -1)
                return;
            
            if (!ValidateLevel(CurrentLevel))
                throw new ArgumentException("Level is out of levels range.");
            
            UpdateStats();
        }

        public bool CanUpgrade() => ValidateLevel(CurrentLevel + 1);

        public void Upgrade()
        {
            CurrentLevel++;
            if (!ValidateLevel(CurrentLevel))
                throw new ArgumentException("Level is out of levels range.");
            
            UpdateStats();
        }
        
        private void UpdateStats()
        {
            var level = Config.Levels[CurrentLevel];
            foreach (var statLevelData in level.NewValues)
            {
                var statModel = _statsSystem.GetStatModel(statLevelData.StatId);
                statModel.UpdateBaseValue(statLevelData.Value);
            }
        }

        private bool ValidateLevel(int level)
        {
            return level < Config.Levels.Length;
        }
    }
}