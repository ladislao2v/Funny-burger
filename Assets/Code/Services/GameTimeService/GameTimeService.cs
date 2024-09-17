using UnityEngine;

namespace Code.Services.GameTimeService
{
    public class GameTimeService : IGameTimeService
    {
        public bool IsPaused => Time.timeScale == 0;
        
        public void StartTime()
        {
            Time.timeScale = 1;
        }

        public void StopTime()
        {
            Time.timeScale = 0;
        }
    }
}