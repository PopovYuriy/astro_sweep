using Core.UI.View;

namespace Core.UI.Controller
{
    public abstract class WidgetControllerBase<TWidgetView> : IWidgetController where TWidgetView : WidgetViewBase
    {
        protected TWidgetView View { get; private set; }

        public void SetView(WidgetViewBase view) => View = view as TWidgetView;

        public abstract void Show();
    }
}