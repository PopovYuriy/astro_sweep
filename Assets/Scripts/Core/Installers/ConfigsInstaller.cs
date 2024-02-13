using Core.GameSystems.AbilitySystem.Data;
using Core.GameSystems.InventorySystem.Data;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Installers/ConfigsInstaller")]
public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
{
    [SerializeField] private InventorySystemConfig _inventorySystemConfig;
    [SerializeField] private AbilitiesConfig _abilitiesConfig;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_inventorySystemConfig);
        Container.BindInstance(_abilitiesConfig);
    }
}