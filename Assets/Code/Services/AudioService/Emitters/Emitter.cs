using UnityEngine;

namespace Code.Services.AudioService.Emitters
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Emitter : MonoBehaviour, ISoundEmitter
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        private void OnValidate() => 
            _audioSource ??= GetComponent<AudioSource>();

        public void Enable() => 
            _audioSource.enabled = true;

        public void Disable() => 
            _audioSource.enabled = false;

        public void SetVolume(float volume) => 
            _audioSource.volume = volume;

        public void Play() => 
            Play(_audioSource, _audioClip);

        public abstract void Play(AudioSource audioSource, AudioClip audioClip);
    }
}