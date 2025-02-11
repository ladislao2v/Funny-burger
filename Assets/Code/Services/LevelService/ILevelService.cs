using System;
using Code.Services.GameDataService;

namespace Code.Services.LevelService
{
    public interface ILevelService : ISavable
    {
        public int Progress { get; }
        public int RequiredProgress { get; }
        public int Current { get; }
        public int Next { get; }

        event Action<int, int> LevelChanged;
        event Action<int, int> ProgressChanged;

        public void AddPoint();
    }
}