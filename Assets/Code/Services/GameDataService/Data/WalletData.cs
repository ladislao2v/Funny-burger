using System;
using Code.Services.ResourceStorage;

namespace Code.Services.GameDataService.Data
{
    [Serializable]
    public class WalletData : IData
    {
        public ResourceType ResourceType { get; set; }
        public int Value { get; set; }

        public WalletData(ResourceType resourceType, int value = 0)
        {
            ResourceType = resourceType;
            Value = value;
        }
    }
}