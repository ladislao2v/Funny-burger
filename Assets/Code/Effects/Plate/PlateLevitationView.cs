using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Code.Effects.Plate
{
    public class PlateLevitationView : MonoBehaviour
    {
        [SerializeField] private float _force;
        [SerializeField] private int _vibrato;
        [SerializeField] private float _duration;
        
        private Sequence _sequence;
        

        private void OnEnable()
        {
            _sequence = DOTween.Sequence();

            _sequence
                .Append(transform.DOShakePosition(_duration, _force, _vibrato))
                .SetLoops(-1);
        }

        private void OnDisable() => 
            _sequence.Kill();
    }
}