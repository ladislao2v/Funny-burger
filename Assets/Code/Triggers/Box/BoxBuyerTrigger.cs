using Code.Effects.BuyZone;
using Code.Services.GameDataService;
using Code.Services.PurchasedBoxesService;
using Code.Services.ResourceStorage;
using Code.Units;
using Code.Units.Commands;
using UnityEngine;
using Zenject;

namespace Code.Triggers.Box
{
    public class BoxBuyerTrigger : Trigger
    {
        [SerializeField] private int _cost;
        [SerializeField] private IngredientBoxTrigger _boxTrigger;
        [SerializeField] private BuyZoneView _buyZoneView;
        [SerializeField] private GameObject _boxView;
        
        private IResourceStorage _storage;
        private IPurchasedBoxesService _purchasedBoxesService;
        private IGameDataService _gameDataService;

        [Inject]
        private void Construct(IResourceStorage storage, IPurchasedBoxesService purchasedBoxesService,
            IGameDataService gameDataService)
        {
            _storage = storage;
            _purchasedBoxesService = purchasedBoxesService;
            _gameDataService = gameDataService;
            _buyZoneView.Construct(_cost);

            if (_purchasedBoxesService.IsPurchased(_boxTrigger.IngredientType))
                ApplyPurchasedState();
        }

        protected override bool TryInteractWith(IPlayer player)
        {
            if(_storage.GetWallet(ResourceType.Coin).TrySpend(_cost) == false)
                return false;
            
            ICommand command = new SpendMoneyForBoxCommand(_storage, _cost);

            player.Do(command, OnBuyCommandExecuted);

            return true;
        }

        private void OnBuyCommandExecuted()
        {
            _purchasedBoxesService.MarkPurchased(_boxTrigger.IngredientType);
            ApplyPurchasedState();
            _gameDataService.SaveData();
        }

        private void ApplyPurchasedState()
        {
            _boxTrigger.enabled = true;
            _boxView.SetActive(true);
            enabled = false;
        }
    }
}
