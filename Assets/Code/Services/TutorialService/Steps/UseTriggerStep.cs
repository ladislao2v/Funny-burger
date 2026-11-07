using Code.Effects.Box;
using Code.Triggers;
using Code.Units;

namespace Code.Services.TutorialService.Steps
{
    public sealed class UseTriggerStep : TutorialStep
    {
        private readonly Trigger _trigger;
        private readonly IPlayer _player;

        public UseTriggerStep(TutorialArrowView arrow, Trigger trigger, IPlayer player) : base(arrow)
        {
            _trigger = trigger;
            _player = player;
        }

        protected override void OnStarted()
        {
            _trigger.InteractionStarted += OnInteractionStarted;
            _trigger.InteractionEnded += OnInteractionEnded;
        }

        protected override void OnEnded()
        {
            _trigger.InteractionStarted -= OnInteractionStarted;
            _trigger.InteractionEnded -= OnInteractionEnded;
        }

        private void OnInteractionStarted()
        {
            if (_player.IsBusy == false)
                Complete();
        }

        private void OnInteractionEnded() =>
            Complete();
    }
}
