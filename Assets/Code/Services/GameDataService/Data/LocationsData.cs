using System;
using System.Collections.Generic;
using Code.Services.LocationsService;

namespace Code.Services.GameDataService.Data
{
    [Serializable]
    public class LocationsData : IData
    {
        public Location CurrentLocation;

        public List<Location> OpenedLocations;
        public List<Location> PurchasedLocations;
    }
}
