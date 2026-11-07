using System;

namespace Code.Services.TutorialService
{
    public interface ITutorialStep
    {
        event Action Completed;

        void StartStep();
        void EndStep();
    }
}
