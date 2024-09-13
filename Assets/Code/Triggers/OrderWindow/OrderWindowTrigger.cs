using Code.Services.BurgerOrderService;
using Code.Units;
using Zenject;

namespace Code.Triggers.OrderWindow
{
    public class OrderWindowTrigger : Trigger<IPlayer>
    {
        private IBurgerOrderService _burgerOrderService;

        [Inject]
        private void Construct(IBurgerOrderService burgerOrderService)
        {
            _burgerOrderService = burgerOrderService;
        }
        
        protected override bool TryInteractWith(IPlayer player)
        {
            if (!_burgerOrderService.TryPassOrder(player.Plate))
                _burgerOrderService.CancelOrder();

            player.Plate.Clear();

            return true;
        }
    }
}