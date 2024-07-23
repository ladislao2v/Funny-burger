using Code.Services.GameDataService;
using UniRx;

namespace Code.Services.WalletService
{
    public interface IWalletService : ISavable
    {
        IReadOnlyReactiveProperty<int> Money { get; }

        public void Add(int value);
        void Spend(int value);
        public bool TrySpend(int value);
    }
}