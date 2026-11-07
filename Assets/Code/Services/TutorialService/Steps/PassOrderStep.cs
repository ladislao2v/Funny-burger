using Code.Effects.Box;
using Code.Services.BurgerOrderService;
using Code.Services.ClientsService;
using Cysharp.Threading.Tasks;

namespace Code.Services.TutorialService.Steps
{
    public sealed class PassOrderStep : TutorialStep
    {
        private readonly IBurgerOrderService _orderService;
        private readonly IClientsService _clientsService;

        public PassOrderStep(TutorialArrowView arrow, IBurgerOrderService orderService,
            IClientsService clientsService) : base(arrow)
        {
            _orderService = orderService;
            _clientsService = clientsService;
        }

        protected override void OnStarted() =>
            _orderService.OrderPassed += OnOrderPassed;

        protected override void OnEnded() =>
            _orderService.OrderPassed -= OnOrderPassed;

        private void OnOrderPassed()
        {
            _clientsService.SendClientAway();
            _clientsService.LoadClients().Forget();

            Complete();
        }
    }
}
