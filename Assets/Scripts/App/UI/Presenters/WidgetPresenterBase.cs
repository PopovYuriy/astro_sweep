using System;
using System.Collections.Generic;
using Core.UI.Controller;
using Core.UI.Presenter;
using Core.UI.Provider;
using Core.UI.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.UI.Presenters
{
    public abstract class WidgetPresenterBase<TWidgetType> : IWidgetPresenter<TWidgetType> where TWidgetType : Enum
    {
        private IWidgetProvider<TWidgetType> _provider;
        private Transform _transform;

        private Dictionary<TWidgetType, IWidgetController> _shownWidgets;
        
        public void Initialize(Transform parentTransform, IWidgetProvider<TWidgetType> provider)
        {
            _transform = parentTransform;
            _provider = provider;

            _shownWidgets = new Dictionary<TWidgetType, IWidgetController>();
        }
        
        public void ShowWidget(Enum id)
        {
            var widgetId = (TWidgetType) id;
            var viewPrefab = _provider.GetWidgetView(widgetId);
            var view = Object.Instantiate<WidgetViewBase>(viewPrefab, _transform);
            var controller = _provider.GetWidgetController(widgetId);
            controller.SetView(view);
            controller.Show();
            
            _shownWidgets.Add(widgetId, controller);
        }

        public void HideWidget(Enum id)
        {
            throw new NotImplementedException();
        }
    }
}