namespace Code.Services.GameDataService
{
    public interface IGameDataService
    {
        void Add(ISavable savable, string key);
        void Remove(ISavable savable);
        void LoadData();
        void SaveData();
    }
}