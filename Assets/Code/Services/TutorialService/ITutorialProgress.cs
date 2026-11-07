using Code.Services.GameDataService;

namespace Code.Services.TutorialService
{
    public interface ITutorialProgress : ISavable
    {
        bool IsStarted { get; }
        bool IsCompleted { get; }
        int CompletedSteps { get; }

        void MarkStarted();
        void CompleteSteps(int count);
        void Complete();
    }
}
