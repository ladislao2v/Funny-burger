namespace Code.Services.TutorialService
{
    public class TutorialServiceProvider : ITutorialServiceProvider
    {
        public ITutorialService TutorialService { get; private set; }
        
        public void Set(ITutorialService tutorialService) => 
            TutorialService = tutorialService;
    }
}