namespace Code.Services.GameDataService
{
    public interface ISavable
    {
        string SaveKey { get; }
        void Load(IData data);
        IData Save();
    }
}