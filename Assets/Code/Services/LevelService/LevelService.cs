using System;
using Code.Services.ConfigProvider;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;
using UnityEngine;

namespace Code.Services.LevelService
{
    public sealed class LevelService : ILevelService
    {
        private readonly IConfigProvider _configProvider;
        
        public int Current { get; private set; }
        public int Next => Current + 1;

        public int Progress { get; private set; }
        public int RequiredProgress => GetRequiredOrders(Current);

        public string SaveKey => nameof(LevelService);
        
        public event Action<int, int> LevelChanged;
        public event Action<int, int> ProgressChanged;

        public LevelService(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }


        public void AddPoint()
        {
            Progress += 1;

            if (Progress == RequiredProgress)
                LevelUp();
            
            ProgressChanged?.Invoke(Progress, RequiredProgress);
        }

        private void LevelUp()
        {
            Current += 1;
            Progress = 0;

            LevelChanged?.Invoke(Current, Next);
            ProgressChanged?.Invoke(Progress, RequiredProgress);
        }

        public void Load(IData data)
        {
            if (data == null)
                data = new LevelData();
            
            if (data is not LevelData levelData)
                throw new ArgumentException(nameof(data));

            Current = levelData.Current;
            Progress = levelData.Progress;
            
            ProgressChanged?.Invoke(Progress, RequiredProgress);
        }

        public IData Save() => 
            new LevelData(Current, Progress);

        private int GetRequiredOrders(int level)
        {
            if (level < 0)
                throw new ArgumentException(nameof(level));

            return GetOrdersToNewLevel(level);
        }

        private int GetOrdersToNewLevel(int level)
        {
            if (level == 0)
                return 1;
            
            if(level == 1)
                return 3;
            
            int orders = (int)Math.Floor(3 * (level - 1) + 2 * Math.Sqrt(level - 1));
            int maxLevelTasks = _configProvider.SettingsConfig.MaxLevelTasks;
            
            if (orders > maxLevelTasks)
                return maxLevelTasks;

            return orders;
        }
    }
}