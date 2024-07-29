using UnityEngine;

namespace Code.Services.AudioService.Audio
{
    public class BackgroundMusic : Audio
    {
        private void Start() =>
            Play();

        protected override void Play(AudioSource audioSource, AudioClip audioClip)
        {
            if(!audioSource.enabled)
                return;
            
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}