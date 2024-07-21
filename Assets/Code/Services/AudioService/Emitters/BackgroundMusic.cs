using System;
using UnityEngine;

namespace Code.Services.AudioService.Emitters
{
    public class BackgroundMusic : Emitter
    {
        private void Start()
        {
            Play();
        }

        public override void Play(AudioSource audioSource, AudioClip audioClip)
        {
            audioSource.clip = audioClip;
        }
    }
}