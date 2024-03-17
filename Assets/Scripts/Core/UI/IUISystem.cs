using System;
using Core.UI.Presenter;

namespace Core.UI
{
    public interface IUISystem
    {
        void RegisterWidgetPresenter<TWidgetType>(IWidgetPresenter<TWidgetType> presenter) where TWidgetType : Enum;
        void ShowWidget<TWidgetType>(TWidgetType id) where TWidgetType : Enum;
        void HideWidget<TWidgetType>(TWidgetType id) where TWidgetType : Enum;
    }
}