using Code.Services.GameDataService;
using UniRx;

namespace Code.Services.AudioService
{
    public interface IAudioService : ISavable
    {
        IReadOnlyReactiveProperty<bool> IsActive { get; }
        IReadOnlyReactiveProperty<float> CurrentVolume { get; }

        void Enable();
        void Disable();
        void SetVolume(float value);
    }
}