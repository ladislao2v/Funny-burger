using System;
using Code.BurgerPlate;
using Code.Configs;
using Code.Services.LevelService;
using Code.Services.ResourceStorage;
using UniRx;
using static Code.Services.ResourceStorage.ResourceType;

namespace Code.Services.BurgerOrderService
{
    public sealed class BurgerOrderService : IBurgerOrderService
    {
        private readonly IResourceStorage _resourceStorage;
        private readonly ILevelService _levelService;
        private readonly IOrderValidator _orderValidator = new OrderValidator();

        private RecipeConfig _currentOrder;
        private IDisposable _timer;
        public bool HasOrder => _currentOrder != null;
        
        public event Action<RecipeConfig> Ordered;
        public event Action Failed;
        public event Action OrderPassed;

        public BurgerOrderService(IResourceStorage resourceStorage, ILevelService levelService)
        {
            _resourceStorage = resourceStorage;
            _levelService = levelService;
        }

        public void Order(RecipeConfig recipe)
        {
            _currentOrder = recipe;

            StartTimer();
            
            Ordered?.Invoke(_currentOrder);
        }

        public void CancelOrder()
        {
            _currentOrder = null;
            Failed?.Invoke();
        }

        public bool TryPassOrder(IBurgerPlate plate)
        {
            if (_currentOrder == null)
                return false;
            
            if (plate.IsEmpty)
                return false;
            
            if (!_orderValidator.Validate(_currentOrder, plate.Ingredients))
                return false;
            
            _resourceStorage
                .GetWallet(Coin)
                .Add(_currentOrder.Price);
            _levelService.AddPoint();
            
            OrderPassed?.Invoke();

            _currentOrder = null;

            return true;
        }

        private void StartTimer()
        {
            if (_timer != null)
                _timer.Dispose();
            
            TimeSpan timerTime =
                TimeSpan.FromSeconds(_currentOrder.CookTime);

            _timer = Observable
                .Timer(timerTime)
                .Subscribe((_) => { CancelOrder(); });
        }
    }
}