using Code.Services.GameDataService;

namespace Code.Services.ResourceStorage
{
    public interface IResourceStorage : ISavable
    {
        IWalletService GetWallet(ResourceType resourceType);
    }
}