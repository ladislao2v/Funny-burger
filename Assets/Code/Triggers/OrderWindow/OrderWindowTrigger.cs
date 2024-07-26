using Code.Services.BurgerOrderService;
using Code.Units;
using Code.Units.Commands;
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
                return false;
            
            var clearPlateCommand = 
                new ClearPlateCommand(player.Plate);
            
            player.Do(clearPlateCommand, Disable);

            return true;
        }
    }
}