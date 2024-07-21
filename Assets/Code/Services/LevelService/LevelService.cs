using System;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;

namespace Code.Services.LevelService
{
    public class LevelService : ILevelService
    {
        private const int K = 1;

        private int _progress;
        
        public int Current { get; private set; }
        public int Next => Current + 1;

        private int RequiredProgress => GetRequiredTasks(Current);

        public string SaveKey => nameof(LevelService);
        
        public event Action<int, int> LevelChanged;
        public event Action<int, int> ProgressChanged;


        public void AddPoint()
        {
            _progress += 1;

            if (_progress == RequiredProgress)
                LevelUp();
            
            ProgressChanged?.Invoke(_progress, RequiredProgress);
        }

        private void LevelUp()
        {
            Current += 1;
            _progress = 0;

            LevelChanged?.Invoke(Current, Next);
        }

        public void Load(IData data)
        {
            if (data == null)
                data = new LevelData();
            
            if (data is not LevelData levelData)
                throw new ArgumentException(nameof(data));

            Current = levelData.Current;
            _progress = levelData.Progress;
            
            LevelChanged?.Invoke(Current, Next);
            ProgressChanged?.Invoke(_progress, RequiredProgress);
        }

        public IData Save() => 
            new LevelData(Current, _progress);

        private int GetRequiredTasks(int level)
        {
            if (level < 0)
                throw new ArgumentException(nameof(level));

            return (int)(K * Math.Pow(Math.Log(K + 1, 2), level - 1));
        }
    }
}