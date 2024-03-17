using App.UI.Enums;
using App.UI.Widgets.Screens.UpgradingScreen;
using Core.UI.Controller;
using Core.UI.View;
using UnityEngine;
using Zenject;

namespace App.UI.Providers
{
    [CreateAssetMenu(fileName = "ScreensProvider", menuName = "UI/ScreensProvider")]
    public sealed class UIWidgetsInstaller : ScriptableObjectInstaller<UIWidgetsInstaller>
    {
        [SerializeField] private WidgetViewBase _upgradingScreen;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_upgradingScreen).WithId(ScreenId.UpgradingScreen);
            Container.Bind<IWidgetController>().WithId(ScreenId.UpgradingScreen).To<UpgradingScreenController>()
                .AsTransient();
        }
    }
}