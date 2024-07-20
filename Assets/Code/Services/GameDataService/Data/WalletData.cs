using System;

namespace Code.Services.GameDataService.Data
{
    [Serializable]
    public class WalletData : IData
    {
        public int Value { get; set; }

        public WalletData(int value)
        {
            Value = value;
        }
    }
}