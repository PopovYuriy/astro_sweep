using System;
using Core.UI.Provider;
using UnityEngine;

namespace Core.UI.Presenter
{
    public interface IWidgetPresenter<TWidgetType> : IWidgetPresenter where TWidgetType : Enum
    {
        void Initialize(Transform parentTransform, IWidgetProvider<TWidgetType> provider);
    }

    public interface IWidgetPresenter
    {
        void ShowWidget(Enum id);
        void HideWidget(Enum id);
    }
}