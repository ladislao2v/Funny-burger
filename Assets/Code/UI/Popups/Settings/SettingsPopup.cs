using System;
using System.Collections.Generic;
using Code.Services.AudioService;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Popups.Settings
{
    public class SettingsPopup : Popup
    {
        private readonly CompositeDisposable _disposable;
        
        [SerializeField] private Toggle _soundToggle;

        private IAudioService _audioService;

        private void Awake()
        {
            _soundToggle.onValueChanged.AddListener(OnClicked);
            _audioService.IsActive
                .Subscribe((value) => _soundToggle.isOn = value)
                .AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _soundToggle.onValueChanged.RemoveAllListeners();
            _disposable.Dispose();
        }

        private void OnClicked(bool value)
        {
            if(value)
                _audioService.Enable();
            else
                _audioService.Disable();    
        }
    }
}