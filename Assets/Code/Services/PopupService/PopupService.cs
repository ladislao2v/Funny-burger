using System;
using Code.Services.Factories.PopupFactory;
using Code.Services.GameTimeService;
using Code.UI.Popups;
using UniRx;
using UnityEngine;

namespace Code.Services.PopupService
{
    public sealed class PopupService : IPopupService
    {
        private const float PopupCreationTime = 0.175f;
        
        private readonly IPopupFactory _popupFactory;
        private readonly IPopupContainer _popupContainer;
        private readonly IGameTimeService _gameTimeService;
        
        private IDisposable _timer;
        private Popup _current;

        public PopupService(IPopupFactory popupFactory, IPopupContainer popupContainer, IGameTimeService gameTimeService)
        {
            _popupFactory = popupFactory;
            _popupContainer = popupContainer;
            _gameTimeService = gameTimeService;
        }
        
        public async void ShowPopup(PopupType popupType)
        {
            _current = 
                await _popupFactory.Create(popupType);

            _current.Clicked += OnPopupClosed;
            
            _popupContainer.Put(_current);
            
            _timer = Observable
                .Timer(TimeSpan.FromSeconds(PopupCreationTime))
                .Subscribe(_ =>
                {
                    _gameTimeService.StopTime();
                    _timer.Dispose();
                });
        }

        private void OnPopupClosed()
        {
            _gameTimeService.StartTime();
            _current.Clicked -= OnPopupClosed;
        }
    }
}