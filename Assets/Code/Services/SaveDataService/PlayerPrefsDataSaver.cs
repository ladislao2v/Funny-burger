using Code.Services.GameDataService;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Services.SaveDataService
{
    public class PlayerPrefsDataSaver : ISaveDataService
    {
        private readonly JsonSerializerSettings _settings = new()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        
        public IData Load(string key)
        {
            string json = PlayerPrefs.GetString(key);

            IData data = JsonConvert.DeserializeObject<IData>(json, _settings);

            return data;
        }

        public void Save(IData data, string key)
        {
            string json = JsonConvert.SerializeObject(data, _settings);
            
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
    }
}