using UnityEngine;

namespace Code.Units.Commands
{
    public class MoveTo : ICommand
    {
        private readonly IClient _client;
        private readonly Vector3 _position;
        private readonly float _duration; 

        public MoveTo(IClient client, Transform point, float duration) 
            : this(client, point.position, duration)
        {
        }

        public MoveTo(IClient client, Vector3 position, float duration)
        {
            _client = client;
            _position = position;
            _duration = duration;
        }

        public void Execute()
        {
            _client.Movement.Move(_position, _duration);
        }
    }
}
