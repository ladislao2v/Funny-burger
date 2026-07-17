using Code.Services.ResourceStorage;

namespace Code.Units.Commands
{
    public class SpendMoneyForBoxCommand : ICommand
    {
        private readonly IResourceStorage _storage;
        private readonly int _value;

        public SpendMoneyForBoxCommand(IResourceStorage storage, int value)
        {
            _storage = storage;
            _value = value;
        }
        public void Execute()
        {
            _storage
                .GetWallet(ResourceType.Coin)
                .Spend(_value);
        }
    }
}