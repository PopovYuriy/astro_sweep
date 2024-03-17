using App.UI.Enums;
using Core.UI.Controller;
using Core.UI.Provider;
using Core.UI.View;
using Zenject;

namespace App.UI.Providers
{
    public sealed class ScreensProvider : IWidgetProvider<ScreenId>
    {
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }
        
        public WidgetViewBase GetWidgetView(ScreenId id)
        {
            var view = _container.TryResolveId<WidgetViewBase>(id);
            return view;
        }

        public IWidgetController GetWidgetController(ScreenId id)
        {
            var controller = _container.TryResolveId<IWidgetController>(id);
            return controller;
        }
    }
}