using System;
using System.Collections.Generic;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;
using static Code.Services.ResourceStorage.ResourceType;

namespace Code.Services.ResourceStorage
{
    public interface IResourceStorage : ISavable
    {
        IWalletService GetWallet(ResourceType resourceType);
    }

    public class ResourceStorage : IResourceStorage
    {
        private readonly Dictionary<ResourceType, IWalletService> _wallets = new()
        {
            [Coin] = new Wallet(),
            [Gem] = new Wallet()
        };

        public string SaveKey => nameof(ResourceStorage);

        public IWalletService GetWallet(ResourceType resourceType) => 
            _wallets[resourceType];

        public void Load(IData data)
        {
            if (data == null)
                return;
            
            if (data is not ResourcesData resourcesData)
                throw new ArgumentException(nameof(data));

            foreach (var walletData in resourcesData.Wallets)
            {
                _wallets[walletData.ResourceType]
                    .Add(walletData.Value);
            }
        }

        public IData Save()
        {
            List<WalletData> walletDatas = new();

            foreach (var (resourceType, wallet) in _wallets)
            {
                walletDatas.Add(new WalletData(resourceType, wallet.Money.Value));
            }
            
            return new ResourcesData(walletDatas);
        }
    }
}