using System;
using System.Collections.Generic;
using Code.Services.AudioService;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Popups.Settings
{
    public sealed class SettingsPopup : Popup
    {
        private readonly CompositeDisposable _disposable = new();
        
        [SerializeField] private Toggle _soundToggle;

        private IAudioService _audioService;
        
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
            
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