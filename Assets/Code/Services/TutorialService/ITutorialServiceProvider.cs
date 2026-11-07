namespace Code.Services.TutorialService
{
    public interface ITutorialServiceProvider
    {
        public ITutorialService TutorialService { get; }
        void Set(ITutorialService tutorialService);
    }
}