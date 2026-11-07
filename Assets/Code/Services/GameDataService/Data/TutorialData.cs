using System;

namespace Code.Services.GameDataService.Data
{
    [Serializable]
    public class TutorialData : IData
    {
        public bool IsStarted { get; set; }
        public bool IsCompleted { get; set; }
        public int CompletedSteps { get; set; }

        public TutorialData(bool isStarted = false, bool isCompleted = false, int completedSteps = 0)
        {
            IsStarted = isStarted;
            IsCompleted = isCompleted;
            CompletedSteps = completedSteps;
        }
    }
}
