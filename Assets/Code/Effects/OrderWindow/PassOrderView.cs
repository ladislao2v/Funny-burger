using Code.Triggers;
using Code.Units;
using UnityEngine;

namespace Code.Effects.OrderWindow
{
    public sealed class PassOrderView : MonoBehaviour
    {
        [SerializeField] private Trigger<IPlayer> _trigger;
        [SerializeField] private ParticleSystem _coins;

        private void Awake() =>
            _trigger.InteractionStarted += _coins.Play;

        private void OnDestroy() => 
            _trigger.InteractionStarted -= _coins.Play;
    }
}