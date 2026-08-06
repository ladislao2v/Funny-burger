using System.Collections.Generic;
using Code.Ingredients;
using Code.Services.LocationsService;

namespace Code.Services.GameDataService.Data
{
    public class LocationsData : IData
    {
        public List<Location> PurchasedLocations { get; set; } = new();

        public LocationsData() { }

        public LocationsData(List<Location> purchasedLocations)
        {
            PurchasedLocations = purchasedLocations;
        }
    }
}