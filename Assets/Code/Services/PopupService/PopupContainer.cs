﻿using System;
using Code.Effects.Popup;
using Code.Services.GameTimeService;
using Code.UI.Popups;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Services.PopupService
{
    public class PopupContainer : MonoBehaviour, IPopupContainer
    {
        private const double PopupCreationTime = 0.175f;
        
        [SerializeField] private Vector3 _popupPosition;
        [SerializeField] private Button _background;

        private Popup _current;
        private IDisposable _timer;
        private IGameTimeService _gameTimeService;


        [Inject]
        private void Construct(IGameTimeService gameTimeService)
        {
            _gameTimeService = gameTimeService;
        }

        public void Put(Popup popup)
        {
            if(_current != null)
                Hide();
            
            _background.onClick.AddListener(Hide);
            _background.gameObject.SetActive(true);

            _current = popup;

            var popupTransform = popup.transform;
            
            popupTransform.SetParent(transform);
            popupTransform.localPosition = _popupPosition;
            
            popup.Clicked += Hide;

            _timer = Observable
                .Timer(TimeSpan.FromSeconds(PopupCreationTime))
                .Subscribe(_ =>
                {
                    _gameTimeService.StopTime();
                    _timer.Dispose();
                });
        }

        private void Hide()
        {
            _gameTimeService.StartTime();
            
            CloseLastPopup();
            
            _background.gameObject.SetActive(false);
            _background.onClick.RemoveAllListeners();
        }

        private void CloseLastPopup()
        {
            _current.Clicked -= Hide;
            _current
                .GetComponent<PopupView>()
                .ScaleDown(_current.Close);
            _current = null;
        }
    }
}