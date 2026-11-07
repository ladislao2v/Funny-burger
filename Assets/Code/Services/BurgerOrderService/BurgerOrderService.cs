using System;
using Code.BurgerPlate;
using Code.Configs;
using Code.Services.ClientLoyaltyService;
using Code.Services.LevelService;
using Code.Services.ResourceStorage;
using UniRx;
using static Code.Services.ResourceStorage.ResourceType;

namespace Code.Services.BurgerOrderService
{
    public sealed class BurgerOrderService : IBurgerOrderService, IDisposable
    {
        private readonly IResourceStorage _resourceStorage;
        private readonly ILevelService _levelService;
        private readonly IClientLoyaltyService _clientLoyaltyService;
        private readonly IOrderValidator _orderValidator = new OrderValidator();

        private RecipeConfig _currentOrder;
        private Func<bool> _hasClient;
        private IDisposable _timer;
        public bool HasOrder => _currentOrder != null;
        public bool HasTimer { get; private set; }
        public float CookTime { get; private set; }

        public event Action<RecipeConfig> Ordered;
        public event Action Failed;
        public event Action OrderPassed;

        public BurgerOrderService(IResourceStorage resourceStorage, ILevelService levelService,
            IClientLoyaltyService clientLoyaltyService)
        {
            _resourceStorage = resourceStorage;
            _levelService = levelService;
            _clientLoyaltyService = clientLoyaltyService;
        }

        public void Order(RecipeConfig recipe, bool withTimer = true)
        {
            _currentOrder = recipe;
            HasTimer = withTimer;
            CookTime = _clientLoyaltyService.GetCookTime(recipe.CookTime);

            if (withTimer)
                StartTimer();
            else
                StopTimer();

            Ordered?.Invoke(_currentOrder);
        }

        public void CancelOrder() => 
            FinishOrder(success: false);

        public bool TryPassOrder(IBurgerPlate plate)
        {
            if (plate.IsEmpty)
                return false;
            
            if (!_orderValidator.Validate(_currentOrder, plate.Ingredients))
                return false;
            
            int price = _currentOrder.Price;
            int tip = _clientLoyaltyService.GetTip(price);
            
            _resourceStorage
                .GetWallet(Coin)
                .Add(price + tip);
            _levelService.AddPoint();
            
            FinishOrder(success: true);

            return true;
        }

        public void Dispose() => 
            _timer?.Dispose();

        private void FinishOrder(bool success)
        {
            if (_currentOrder == null)
                return;

            StopTimer();
            _currentOrder = null;

            if (success)
                OrderPassed?.Invoke();
            else
                Failed?.Invoke();
        }

        private void StartTimer()
        {
            if (_timer != null)
                _timer.Dispose();
            
            TimeSpan timerTime =
                TimeSpan.FromSeconds(CookTime);

            _timer = Observable
                .Timer(timerTime)
                .Subscribe((_) => { CancelOrder(); });
        }

        private void StopTimer()
        {
            _timer?.Dispose();
            _timer = null;
        }
    }
}