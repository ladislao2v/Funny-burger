using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Services.AudioService.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Audio : MonoBehaviour, ISoundEmitter
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;
        
        private CompositeDisposable _disposables = new();

        private void OnValidate() => 
            _audioSource ??= GetComponent<AudioSource>();

        [Inject]
        private void Construct(IAudioService audioService)
        {
            audioService.IsActive.Subscribe(SetActive).AddTo(_disposables);
            audioService.CurrentVolume.Subscribe(SetVolume).AddTo(_disposables);
        }

        private void OnDestroy() => 
            _disposables.Dispose();

        public void SetActive(bool value) =>
            _audioSource.mute = !value;

        public void SetVolume(float volume) => 
            _audioSource.volume = volume;

        public void Play()
        {
            Play(_audioSource, _audioClip);
        }

        protected abstract void Play(AudioSource audioSource, AudioClip audioClip);
    }
}