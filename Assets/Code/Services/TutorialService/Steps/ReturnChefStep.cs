using Code.Units;
using UnityEngine;

namespace Code.Services.TutorialService.Steps
{
    public sealed class ReturnChefStep : TutorialStep
    {
        private readonly IPlayer _player;
        private readonly Vector3 _startPosition;

        public ReturnChefStep(IPlayer player, Vector3 startPosition)
        {
            _player = player;
            _startPosition = startPosition;
        }

        protected override void OnStarted()
        {
            _player.Teleport(_startPosition);

            Complete();
        }

        protected override void OnEnded() { }
    }
}
