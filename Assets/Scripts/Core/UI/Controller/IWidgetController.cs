using Core.UI.View;

namespace Core.UI.Controller
{
    public interface IWidgetController
    {
        void SetView(WidgetViewBase view);
        void Show();
    }
}