using System;
using System.Collections.Generic;
using Code.Configs;

namespace Code.Services.LocationsService
{
    public interface ILocationsService
    {
        LocationConfig CurrentLocation { get; }
        IEnumerable<LocationConfig> OpenedLocations { get; }
        IEnumerable<LocationConfig> PurchasedLocations { get; }

        event Action<LocationConfig> LocationOpened;
        event Action<LocationConfig> LocationChanged;
        void OpenNewLocation(LocationConfig locationConfig);
        bool TryPurchaseLocation(LocationConfig locationConfig);
        bool TryUseLocation(Location location);
    }
}
