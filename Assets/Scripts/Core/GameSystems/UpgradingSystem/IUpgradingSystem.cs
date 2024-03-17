using System;
using Core.GameSystems.UpgradingSystem.Items;

namespace Core.GameSystems.UpgradingSystem
{
    public interface IUpgradingSystem<T> where T : Enum
    {
        IUpgradableItem GetItem(T type);
        void UpgradeItem(T type);
    }
}