﻿using System;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;
using UniRx;

namespace Code.Services.WalletService
{
    public sealed class Wallet : IWalletService
    {
        private readonly ReactiveProperty<int> _money = new();

        public IReadOnlyReactiveProperty<int> Money => _money;

        public string SaveKey => nameof(Wallet);

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

        public void Load(IData data)
        {
            if (data == null)
                data = new WalletData();
            
            if (data is not WalletData walletData)
                throw new ArgumentException(nameof(data));

            _money.Value = walletData.Value;
        }

        public IData Save() => 
            new WalletData(_money.Value);
    }
}