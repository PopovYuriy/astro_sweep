using System;
using Core.UI.Controller;
using Core.UI.View;

namespace Core.UI.Provider
{
    public interface IWidgetProvider<in TWidgetType> where TWidgetType : Enum
    {
        WidgetViewBase GetWidgetView(TWidgetType id);
        IWidgetController GetWidgetController(TWidgetType id);
    }
}