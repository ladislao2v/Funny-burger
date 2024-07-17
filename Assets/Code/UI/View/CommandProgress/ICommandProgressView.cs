namespace Code.UI.View.CommandProgress
{
    public interface ICommandProgressView : IView
    {
        void OnCommandProgressed(float progress);
    }
}