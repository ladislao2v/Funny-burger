using UnityEngine;

namespace Code.Effects.OrderWindow
{
    public abstract class ClientEmotionView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _emotion;

        protected void Play() => 
            _emotion.Play();
    }
}