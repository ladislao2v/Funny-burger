using System;
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

        private RecipeConfig _currentOrder;
        private IDisposable _timer;
        public bool HasOrder => _currentOrder != null;
        
        public event Action<RecipeConfig> Ordered;
        public event Action Failed;
        public event Action OrderPassed;

        public BurgerOrderService(IWalletService walletService, ILevelService levelService)
        {
            _walletService = walletService;
            _levelService = levelService;
        }

        public void Order(RecipeConfig recipe)
        {
            _currentOrder = recipe;

            StartTimer();
            
            Ordered?.Invoke(_currentOrder);
        }

        private void StartTimer()
        {
            TimeSpan timerTime =
                TimeSpan.FromSeconds(_currentOrder.CookTime);

            _timer = Observable
                .Timer(timerTime)
                .Subscribe((_) =>
                {
                    _currentOrder = null;
                    Failed?.Invoke();
                    _timer.Dispose();
                });
        }

        public bool TryPassOrder(IBurgerPlate plate)
        {
            if (_currentOrder == null)
                return false;
            
            if (plate.IsEmpty)
                return false;
            
            if (!_orderValidator.Validate(_currentOrder, plate.Ingredients))
                return false;
            
            _walletService.Add(_currentOrder.Price);
            _levelService.AddPoint();
            
            OrderPassed?.Invoke();

            _currentOrder = null;

            return true;
        }
    }
}