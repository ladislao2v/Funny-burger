using System.Collections.Generic;
using Code.Services.GameDataService;

namespace Code.Services.SaveDataService
{
    public interface ISaveDataService
    {
        IData Load(string key);
        void Save(IData data, string key);
    }
}