using System;
using UniRx;

namespace Code.Services.ResourceStorage
{
    public sealed class Wallet : IWalletService
    {
        private readonly ReactiveProperty<int> _money = new();

        public IReadOnlyReactiveProperty<int> Money => _money;

        public void Add(int value)
        {
            if (value < 0)
                throw new ArgumentException(nameof(value));

            _money.Value += value;
        }

        public void Spend(int value)
        {
            if(!TrySpend(value))
                return;
            
            _money.Value -= value;
        }

        public bool TrySpend(int value)
        {
            if (value < 0)
                throw new ArgumentException(nameof(value));

            if (_money.Value < value)
                return false;

            return true;
        }
    }
}