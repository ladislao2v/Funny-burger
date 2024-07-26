using Code.Triggers;
using Code.Units;
using UnityEngine;

namespace Code.Effects.Interactor
{
    public class SteamView : MonoBehaviour
    {
        [SerializeField] private Trigger<IPlayer> _trigger;
        [SerializeField] private ParticleSystem _steam;

        private void OnEnable()
        {
            _trigger.Enter += _steam.Play;
            _trigger.Exit += _steam.Stop;
        }

        private void OnDisable()
        {
            _trigger.Enter -= _steam.Play;
            _trigger.Exit -= _steam.Stop;
        }
    }
}