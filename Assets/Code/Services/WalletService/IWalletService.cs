using UniRx;

namespace Code.Services.WalletService
{
    public interface IWalletService
    {
        IReadOnlyReactiveProperty<int> Money { get; }

        public void Add(int value);
        public bool TrySpend(int value);
    }
}