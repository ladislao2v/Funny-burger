using Code.Services.BurgerOrderService;
using Zenject;

namespace Code.Effects.OrderWindow
{
    public sealed class ClientPositiveEmotionView : ClientEmotionView
    {
        private IBurgerOrderService _burgerOrderService;

        [Inject]
        private void Construct(IBurgerOrderService burgerOrderService)
        {
            _burgerOrderService = burgerOrderService;
        }

        private void OnEnable() => 
            _burgerOrderService.OrderPassed += Play;

        private void OnDisable() => 
            _burgerOrderService.OrderPassed -= Play;
    }
}