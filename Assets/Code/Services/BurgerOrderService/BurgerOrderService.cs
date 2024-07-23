using Code.BurgerPlate;
using Code.Configs;
using Code.Services.LevelService;
using Code.Services.WalletService;
using UniRx;

namespace Code.Services.BurgerOrderService
{
    public sealed class BurgerOrderService : IBurgerOrderService
    {
        private readonly IWalletService _walletService;
        private readonly ILevelService _levelService;
        private readonly IOrderValidator _orderValidator = new OrderValidator();
        private readonly ReactiveProperty<RecipeConfig> _currentOrder = new();

        public IReadOnlyReactiveProperty<RecipeConfig> CurrentOrder => _currentOrder;

        public BurgerOrderService(IWalletService walletService, ILevelService levelService)
        {
            _walletService = walletService;
            _levelService = levelService;
        }

        public void Order(RecipeConfig recipe) => 
            _currentOrder.Value = recipe;

        public bool TryPassOrder(IBurgerPlate plate)
        {
            if (!_orderValidator.Validate(_currentOrder.Value, plate.Ingredients))
                return false;
            
            _walletService.Add(_currentOrder.Value.Price);
            _levelService.AddPoint();

            return true;
        }
    }
}