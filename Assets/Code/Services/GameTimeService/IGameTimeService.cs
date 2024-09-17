namespace Code.Services.GameTimeService
{
    public interface IGameTimeService
    {
        bool IsPaused { get; }
    
        void StartTime();
        void StopTime();
    }
}
