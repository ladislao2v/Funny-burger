using System;
using System.Collections.Generic;
using Code.Services.SaveDataService;

namespace Code.Services.GameDataService
{
    public class GameDataService : IGameDataService
    {
        private readonly ISaveDataService _saveDataService;
        private readonly Dictionary<ISavable, string> _keys = new();

        public GameDataService(ISaveDataService saveDataService)
        {
            _saveDataService = saveDataService;
        }

        public void Add(ISavable savable, string key)
        {
            if (_keys.ContainsKey(savable))
                throw new ArgumentException(nameof(savable));
            
            _keys.Add(savable, key);
        }

        public void Remove(ISavable savable)
        {
            if (!_keys.ContainsKey(savable))
                throw new ArgumentException(nameof(savable));
            
            _keys.Remove(savable);
        }

        public void LoadData()
        {
            foreach ((ISavable savable, string key) in _keys)
            {
                IData data = _saveDataService.Load(key);
                
                savable.Load(data);
            }
        }

        public void SaveData()
        {
            foreach ((ISavable savable, string key) in _keys)
            {
                IData data = savable.Save();
                
                _saveDataService.Save(data, key);
            }
        }
    }
}