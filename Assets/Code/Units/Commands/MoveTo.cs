using System;
using Code.Movement;
using DG.Tweening;
using UnityEngine;

namespace Code.Units.Commands
{
    public class MoveTo : ICommand
    {
        private readonly IClient _client;
        private readonly Transform _point;
        private readonly float _duration; 

        public MoveTo(IClient client, Transform point, float duration)
        {
            _client = client;
            _point = point;
            _duration = duration;
        }

        public void Execute()
        {
            _client.Movement.Move(_point.position, _duration);
        }
    }
}
