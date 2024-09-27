using Code.Triggers;
using UnityEngine;

namespace Code.Effects.OrderWindow
{
    public sealed class PassOrderView : MonoBehaviour
    {
        [SerializeField] private Trigger _trigger;
        [SerializeField] private ParticleSystem _coins;

        private void Awake() =>
            _trigger.InteractionStarted += _coins.Play;

        private void OnDestroy() => 
            _trigger.InteractionStarted -= _coins.Play;
    }
}