using System;
using Code.Effects.Box;

namespace Code.Services.TutorialService.Steps
{
    public abstract class TutorialStep : ITutorialStep
    {
        private readonly TutorialArrowView _arrow;

        private bool _isActive;

        public event Action Completed;

        protected TutorialStep(TutorialArrowView arrow = null) =>
            _arrow = arrow;

        public void StartStep()
        {
            if (_isActive)
                return;

            _isActive = true;

            if (_arrow != null)
                _arrow.Show();

            OnStarted();
        }

        public void EndStep()
        {
            if (!_isActive)
                return;

            _isActive = false;

            if (_arrow != null)
                _arrow.Hide();

            OnEnded();
        }

        protected void Complete()
        {
            if (_isActive)
                Completed?.Invoke();
        }

        protected abstract void OnStarted();
        protected abstract void OnEnded();
    }
}
