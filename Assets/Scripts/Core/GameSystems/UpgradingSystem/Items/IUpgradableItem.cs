using Core.GameSystems.StatsSystem;
using Core.GameSystems.UpgradingSystem.Data;

namespace Core.GameSystems.UpgradingSystem.Items
{
    public interface IUpgradableItem
    {
        int CurrentLevel { get; }
        UpgradingStatConfig Config { get; }
        void Initialize(int currentLevel, CharacterStatsSystem statsSystem, UpgradingStatConfig upgradingConfig);
        bool CanUpgrade();
        void Upgrade();
    }
}