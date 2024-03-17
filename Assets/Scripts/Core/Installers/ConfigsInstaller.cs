using Core.GameSystems.AbilitySystem.Data;
using Core.GameSystems.InventorySystem.Data;
using Core.GameSystems.StatsSystem.Data;
using Core.GameSystems.UpgradingSystem.Data;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Installers/ConfigsInstaller")]
public sealed class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
{
    [SerializeField] private InventorySystemConfig _inventorySystemConfig;
    [SerializeField] private AbilitiesConfig _abilitiesConfig;
    [SerializeField] private StatsConfig _statsConfig;
    [SerializeField] private UpgradingConfigsStorage _upgradingConfigsStorage;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_inventorySystemConfig);
        Container.BindInstance(_abilitiesConfig);
        Container.BindInstance(_statsConfig);
        Container.BindInstance(_upgradingConfigsStorage);
    }
}