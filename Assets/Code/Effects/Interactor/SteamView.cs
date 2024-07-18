using Code.Kitchen;
using Code.Units;
using UnityEngine;

namespace Code.Effects.Interactor
{
    public class SteamView : MonoBehaviour
    {
        [SerializeField] private Interactor<IPlayer> _interactor;
        [SerializeField] private ParticleSystem _steam;

        private void OnEnable()
        {
            _interactor.Enter += _steam.Play;
            _interactor.Exit += _steam.Stop;
        }

        private void OnDisable()
        {
            _interactor.Enter -= _steam.Play;
            _interactor.Exit -= _steam.Stop;
        }
    }
}