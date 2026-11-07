using System;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;

namespace Code.Services.TutorialService
{
    public sealed class TutorialProgress : ITutorialProgress
    {
        public bool IsStarted { get; private set; }
        public bool IsCompleted { get; private set; }
        public int CompletedSteps { get; private set; }

        public string SaveKey => nameof(TutorialProgress);

        public void MarkStarted() =>
            IsStarted = true;

        public void CompleteSteps(int count)
        {
            if (count < 0)
                throw new ArgumentException(nameof(count));

            CompletedSteps = Math.Max(CompletedSteps, count);
        }

        public void Complete()
        {
            IsStarted = true;
            IsCompleted = true;
        }

        public void Load(IData data)
        {
            if (data == null)
                data = new TutorialData();

            if (data is not TutorialData tutorialData)
                throw new ArgumentException(nameof(data));

            IsStarted = tutorialData.IsStarted;
            IsCompleted = tutorialData.IsCompleted;
            CompletedSteps = tutorialData.CompletedSteps;
        }

        public IData Save() =>
            new TutorialData(IsStarted, IsCompleted, CompletedSteps);
    }
}
