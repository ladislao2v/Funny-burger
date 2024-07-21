using System;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;
using UniRx;

namespace Code.Services.AudioService
{
    public class AudioService : IAudioService
    {
        private readonly ISoundEmitter[] _soundEmitters;
        private readonly ReactiveProperty<bool> _isActive = new();
        private readonly ReactiveProperty<float> _currentVolume = new();

        public IReadOnlyReactiveProperty<bool> IsActive => _isActive;
        public IReadOnlyReactiveProperty<float> CurrentVolume => _currentVolume;

        public string SaveKey => nameof(AudioService);

        public AudioService(ISoundEmitter[] soundEmitters)
        {
            _soundEmitters = soundEmitters;
        }

        public void Enable()
        {
            foreach (var soundEmitter in _soundEmitters)
                soundEmitter.Enable();

            _isActive.Value = true;
        }

        public void Disable()
        {
            foreach (var soundEmitter in _soundEmitters)
                soundEmitter.Disable();
            
            _isActive.Value = false;
        }

        public void SetVolume(float value)
        {
            if (value < 0 || value > 1)
                throw new ArgumentException(nameof(value));

            _currentVolume.Value = value;
            
            foreach (var soundEmitter in _soundEmitters)
                soundEmitter.SetVolume(value);    
        }

        public void Load(IData data)
        {
            if (data == null)
                data = new AudioData();
            
            if (data is not AudioData audioData)
                throw new ArgumentException(nameof(data));

            _isActive.Value = audioData.IsActive;
            _currentVolume.Value = audioData.CurrentVolume;
            
            if(_isActive.Value)
                Enable();
            else
                Disable();
        }

        public IData Save() =>
            new AudioData(_isActive.Value, _currentVolume.Value);
    }
}