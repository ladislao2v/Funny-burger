namespace Code.UI.TaskProgress
{
    public interface ITaskProgressView : IView
    {
        void OnCommandProgressed(float duration);
    }
}