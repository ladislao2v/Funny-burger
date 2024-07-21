using UnityEngine;

namespace Code.Services.AudioService.Emitters
{
    public class ButtonClickSound : Emitter
    {
        public override void Play(AudioSource audioSource, AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}