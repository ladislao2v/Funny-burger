using Code.Services.WalletService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.UI.Wallet
{
    public class WalletPresenter : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();
        
        private IWalletService _model;
        private IWalletView _view;

        [Inject]
        private void Construct(IWalletService walletService)
        {
            _model = walletService;
            _view = GetComponent<IWalletView>();

            _model.Money
                .Subscribe(_view.OnValueChanged)
                .AddTo(_disposable);
        }

        private void OnDestroy() => 
            _disposable.Dispose();
    }
}