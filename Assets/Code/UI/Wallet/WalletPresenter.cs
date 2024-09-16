using Code.Services.ResourceStorage;
using UniRx;
using UnityEngine;
using Zenject;
using ResourceType = Code.Services.ResourceStorage.ResourceType;

namespace Code.UI.Wallet
{
    public class WalletPresenter : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;
        
        private readonly CompositeDisposable _disposable = new();
        
        private IResourceStorage _model;
        private IWalletView _view;

        [Inject]
        private void Construct(IResourceStorage resourceStorage)
        {
            _model = resourceStorage;
            _view = GetComponent<IWalletView>();

            _model.GetWallet(_resourceType).Money
                .Subscribe(_view.OnValueChanged)
                .AddTo(_disposable);
        }

        private void OnDestroy() => 
            _disposable.Dispose();
    }
}