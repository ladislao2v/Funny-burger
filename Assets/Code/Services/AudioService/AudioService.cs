using System;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;
using UniRx;

namespace Code.Services.AudioService
{
    public sealed class AudioService : IAudioService
    {
        private readonly ReactiveProperty<bool> _isActive = new(true);
        private readonly ReactiveProperty<float> _currentVolume = new(1f);

        public IReadOnlyReactiveProperty<bool> IsActive => _isActive;
        public IReadOnlyReactiveProperty<float> CurrentVolume => _currentVolume;

        public string SaveKey => nameof(AudioService);

        public void Enable() => 
            _isActive.Value = true;

        public void Disable() => 
            _isActive.Value = false;

        public void SetVolume(float value)
        {
            if (value < 0 || value > 1)
                throw new ArgumentException(nameof(value));

            _currentVolume.Value = value;
        }

        public void Load(IData data)
        {
            if (data == null)
                data = new AudioData(true, 1f);
            
            if (data is not AudioData audioData)
                throw new ArgumentException(nameof(data));

            _isActive.Value = audioData.IsActive;
            _currentVolume.Value = audioData.CurrentVolume;
        }

        public IData Save() =>
            new AudioData(_isActive.Value, _currentVolume.Value);
    }
}