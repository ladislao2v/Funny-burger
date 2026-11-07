using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Services.ConfigProvider;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;

namespace Code.Services.LocationsService
{
    public class LocationsService : ILocationsService, ISavable
    {
        private readonly HashSet<LocationConfig> _openedLocations = new();
        private readonly HashSet<LocationConfig> _purchasedLocations = new();

        private readonly IConfigProvider _configProvider;

        public LocationConfig CurrentLocation { get; private set; }

        public string SaveKey => nameof(LocationsService);

        public event Action<LocationConfig> LocationOpened;
        public event Action<LocationConfig> LocationChanged;

        public IEnumerable<LocationConfig> OpenedLocations => _openedLocations;
        public IEnumerable<LocationConfig> PurchasedLocations => _purchasedLocations;

        public LocationsService(IConfigProvider configProvider)
        {
            _configProvider = configProvider;

            LocationConfig initialLocation = _configProvider.GetLocationConfig(Location.Location1);

            OpenNewLocation(initialLocation);
            TryPurchaseLocation(initialLocation);
            ChangeLocation(initialLocation);
        }

        public void OpenNewLocation(LocationConfig locationConfig)
        {
            if (!_openedLocations.Add(locationConfig))
                return;

            LocationOpened?.Invoke(locationConfig);
        }

        public bool TryPurchaseLocation(LocationConfig locationConfig)
        {
            if (!_openedLocations.Contains(locationConfig))
                return false;

            return _purchasedLocations.Add(locationConfig);
        }

        public bool TryUseLocation(Location location)
        {
            var locationConfig = _purchasedLocations.FirstOrDefault(x => x.LocationId == location);

            if (locationConfig == null)
                return false;

            if (locationConfig == CurrentLocation)
                return true;

            ChangeLocation(locationConfig);

            return true;
        }

        private void ChangeLocation(LocationConfig locationConfig)
        {
            CurrentLocation = locationConfig;

            LocationChanged?.Invoke(locationConfig);
        }

        public void Load(IData data)
        {
            if (data == null)
                return;

            if (data is not LocationsData locationsData)
                throw new ArgumentException(nameof(data));

            List<Location> openedLocations = locationsData.OpenedLocations;
            List<Location> purchasedLocations = locationsData.PurchasedLocations;

            foreach (var locationConfig in _configProvider
                         .GetLocationConfigs()
                         .Where(x => openedLocations.Contains(x.LocationId))
                         .ToList())
            {
                _openedLocations.Add(locationConfig);
            }

            foreach (var locationConfig in _configProvider
                         .GetLocationConfigs()
                         .Where(x => purchasedLocations.Contains(x.LocationId))
                         .ToList())
            {
                _purchasedLocations.Add(locationConfig);
            }

            CurrentLocation = _purchasedLocations
                .FirstOrDefault(x => x.LocationId == locationsData.CurrentLocation) ?? CurrentLocation;

            LocationChanged?.Invoke(CurrentLocation);
        }

        public IData Save() =>
            new LocationsData()
            {
                CurrentLocation = CurrentLocation.LocationId,
                OpenedLocations = _openedLocations.Select(x => x.LocationId).ToList(),
                PurchasedLocations = _purchasedLocations.Select(x => x.LocationId).ToList(),
            };
    }
}
