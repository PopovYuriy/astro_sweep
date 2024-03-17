using System;
using System.Collections.Generic;
using Core.UI.Presenter;

namespace Core.UI
{
    public sealed class UISystem : IUISystem
    {
        private Dictionary<Type, IWidgetPresenter> _presenters;

        public UISystem()
        {
            _presenters = new Dictionary<Type, IWidgetPresenter>();
        }

        public void RegisterWidgetPresenter<TWidgetType>(IWidgetPresenter<TWidgetType> presenter) where TWidgetType : Enum
        {
            var type = typeof(TWidgetType);
            if (!_presenters.TryAdd(type, presenter))
                throw new ArgumentException($"Widget presenter with type {type} is already registered");
        }

        public void ShowWidget<TWidgetType>(TWidgetType id) where TWidgetType : Enum
        {
            var presenter = GetWidgetPresenter(id.GetType());
            presenter.ShowWidget(id);
        }

        public void HideWidget<TWidgetType>(TWidgetType id) where TWidgetType : Enum
        {
            var presenter = GetWidgetPresenter(id.GetType());
            presenter.HideWidget(id);
        }

        private IWidgetPresenter GetWidgetPresenter(Type type)
        {
            if (_presenters.TryGetValue(type, out var presenter))
                return presenter;

            throw new ArgumentException($"Widget presenter with type {type} is not registered.");
        }
    }
}