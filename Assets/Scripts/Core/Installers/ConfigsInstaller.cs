using Core.GameSystems.InventorySystem.Data;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Installers/ConfigsInstaller")]
public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
{
    [SerializeField] private InventorySystemConfig _inventorySystemConfig;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_inventorySystemConfig);
    }
}