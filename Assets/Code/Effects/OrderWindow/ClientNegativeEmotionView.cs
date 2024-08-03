using Code.Services.BurgerOrderService;
using Zenject;

namespace Code.Effects.OrderWindow
{
    public sealed class ClientNegativeEmotionView : ClientEmotionView
    {
        private IBurgerOrderService _burgerOrderService;

        [Inject]
        private void Construct(IBurgerOrderService burgerOrderService)
        {
            _burgerOrderService = burgerOrderService;
        }

        private void OnEnable() => 
            _burgerOrderService.Failed += Play;

        private void OnDisable() => 
            _burgerOrderService.Failed -= Play;
    }
}