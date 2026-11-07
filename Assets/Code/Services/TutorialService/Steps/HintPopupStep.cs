using System.Threading;
using Code.Configs;
using Code.Services.LocalizationService;
using Code.Services.PopupService;
using Code.UI.Popups;
using Code.UI.Popups.Reward;
using Cysharp.Threading.Tasks;

namespace Code.Services.TutorialService.Steps
{
    public sealed class HintPopupStep : TutorialStep
    {
        private readonly IPopupService _popupService;
        private readonly IPopupContainerProvider _popupContainerProvider;
        private readonly ILocalizationService _localizationService;
        private readonly TutorialHintConfig _hint;

        private CancellationTokenSource _cancellation;

        public HintPopupStep(IPopupService popupService, IPopupContainerProvider popupContainerProvider,
            ILocalizationService localizationService, TutorialHintConfig hint)
        {
            _popupService = popupService;
            _popupContainerProvider = popupContainerProvider;
            _localizationService = localizationService;
            _hint = hint;
        }

        protected override void OnStarted()
        {
            _cancellation = new CancellationTokenSource();

            ShowHint(_cancellation.Token).Forget();
        }

        protected override void OnEnded()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;
        }

        private async UniTaskVoid ShowHint(CancellationToken token)
        {
            await UniTask.WaitUntil(() => _popupContainerProvider.Container.HasPopup == false,
                cancellationToken: token);

            Language language = _localizationService.Current;

            RewardData data = new RewardData()
            {
                Name = _hint.Title[language],
                Description = _hint.Description[language],
            };

            Popup popup = await _popupService.ShowPopup(PopupType.TutorialHint, data);

            await UniTask.WaitUntil(() => popup == null, cancellationToken: token);

            Complete();
        }
    }
}
