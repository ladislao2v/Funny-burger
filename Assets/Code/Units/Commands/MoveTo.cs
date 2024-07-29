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
        private readonly Action _moved;

        private readonly float _duration = 1f; 

        public MoveTo(IClient client, Transform point, Action moved = null)
        {
            _client = client;
            _point = point;
            _moved = moved;
        }

        public void Execute()
        {
            _client.Movement.Move(_point.position, _duration, _moved);
        }
    }
}
