using UnityEngine;

namespace Code.Services.AudioService.Audio
{
    public class ButtonClickSound : Audio
    {
        protected override void Play(AudioSource audioSource, AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}