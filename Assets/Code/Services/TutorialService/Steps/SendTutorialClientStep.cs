using System.Threading;
using Code.Configs;
using Code.Services.BurgerOrderService;
using Code.Services.ClientsService;
using Cysharp.Threading.Tasks;

namespace Code.Services.TutorialService.Steps
{
    public sealed class SendTutorialClientStep : TutorialStep
    {
        private readonly IClientsService _clientsService;
        private readonly IBurgerOrderService _orderService;
        private readonly RecipeConfig _recipe;

        private CancellationTokenSource _cancellation;

        public SendTutorialClientStep(IClientsService clientsService, IBurgerOrderService orderService,
            RecipeConfig recipe)
        {
            _clientsService = clientsService;
            _orderService = orderService;
            _recipe = recipe;
        }

        protected override void OnStarted()
        {
            _cancellation = new CancellationTokenSource();

            SendClient(_cancellation.Token).Forget();
        }

        protected override void OnEnded()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;
        }

        private async UniTaskVoid SendClient(CancellationToken token)
        {
            await _clientsService.LoadTutorialClient();

            if (token.IsCancellationRequested)
                return;

            _clientsService.SendClientToWindow();

            await UniTask.WaitUntil(() => _clientsService.IsSend, cancellationToken: token);

            _orderService.Order(_recipe, withTimer: false);

            Complete();
        }
    }
}
