using Code.Services.GameDataService;
using UniRx;

namespace Code.Services.ResourceStorage
{
    public interface IWalletService
    {
        IReadOnlyReactiveProperty<int> Money { get; }

        public void Add(int value);
        void Spend(int value);
        public bool TrySpend(int value);
    }
}