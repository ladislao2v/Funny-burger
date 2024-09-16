using System;
using System.Collections.Generic;

namespace Code.Services.GameDataService.Data
{
    [Serializable]
    public class ResourcesData : IData
    {
        public IEnumerable<WalletData> Wallets { get; set; }

        public ResourcesData(IEnumerable<WalletData> wallets)
        {
            Wallets = wallets;
        }
    }
}