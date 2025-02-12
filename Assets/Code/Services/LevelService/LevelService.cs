﻿using System;
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
        public int RequiredProgress => GetRequiredTasks(Current);

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
        }

        public void Load(IData data)
        {
            if (data == null)
                data = new LevelData();
            
            if (data is not LevelData levelData)
                throw new ArgumentException(nameof(data));

            Current = levelData.Current;
            Progress = levelData.Progress;
            
            LevelChanged?.Invoke(Current, Next);
            ProgressChanged?.Invoke(Progress, RequiredProgress);
        }

        public IData Save() => 
            new LevelData(Current, Progress);

        private int GetRequiredTasks(int level)
        {
            if (level < 0)
                throw new ArgumentException(nameof(level));

            if (level == 0)
                return 1;

            int tasks = (int) Math.Pow(2, level - 1) + 1;
            int maxLevelTasks = _configProvider.SettingsConfig.MaxLevelTasks;

            if (tasks > maxLevelTasks)
                return maxLevelTasks;

            return tasks;
        }
    }
}