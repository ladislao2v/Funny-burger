using Code.Services.GameDataService;

namespace Code.Services.LocationsService
{
    public interface ILocationsService
    {
        
    }

    public class LocationsService : ILocationsService, ISavable
    {
        public string SaveKey { get; }
        
        public void Load(IData data)
        {
            throw new System.NotImplementedException();
        }

        public IData Save()
        {
            throw new System.NotImplementedException();
        }
    }
}