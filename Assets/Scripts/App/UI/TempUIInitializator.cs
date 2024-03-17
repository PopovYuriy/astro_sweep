using App.UI.Presenters;
using App.UI.Providers;
using Core.UI;
using UnityEngine;
using Zenject;

namespace App.UI
{
    public sealed class TempUIInitializator : MonoBehaviour
    {
        [SerializeField] private Transform _screensParentTransform;
        
        [Inject] private IUISystem _uiSystem;
        [Inject] private ScreensProvider _screensProvider;

        private void Start()
        {
            var screensPresenter = new ScreenPresenter();
            screensPresenter.Initialize(_screensParentTransform, _screensProvider);
            
            _uiSystem.RegisterWidgetPresenter(screensPresenter);
        }
    }
}