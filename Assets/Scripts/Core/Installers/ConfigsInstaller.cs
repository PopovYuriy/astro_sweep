using Core.GameSystems.AbilitySystem.Data;
using Core.GameSystems.InventorySystem.Data;
using Core.GameSystems.StatsSystem.Data;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Installers/ConfigsInstaller")]
public sealed class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
{
    [SerializeField] private InventorySystemConfig _inventorySystemConfig;
    [SerializeField] private AbilitiesConfig _abilitiesConfig;
    [SerializeField] private StatsConfig _statsConfig;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_inventorySystemConfig);
        Container.BindInstance(_abilitiesConfig);
        Container.BindInstance(_statsConfig);
    }
}