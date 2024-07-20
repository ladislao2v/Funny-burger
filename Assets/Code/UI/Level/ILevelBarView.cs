namespace Code.UI.Level
{
    public interface ILevelBarView : IView
    {
        void OnProgressChanged(int from, int to);
        void OnLevelChanged(int current, int next);
    }
}