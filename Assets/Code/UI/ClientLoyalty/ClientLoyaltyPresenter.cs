using System;
using Code.Services.ClientLoyaltyService;
using UnityEngine;
using Zenject;

namespace Code.UI.ClientLoyalty
{
    public class ClientLoyaltyPresenter : MonoBehaviour
    {
        private IClientLoyaltyService _model;
        private IClientLoyaltyView _view;

        [Inject]
        private void Construct(IClientLoyaltyService clientLoyaltyService)
        {
            _model = clientLoyaltyService;
            _view = GetComponent<IClientLoyaltyView>();
            
            _model.LoyaltyChanged += _view.OnLoyaltyChanged;
            
            _view.OnLoyaltyChanged(_model.Loyalty);
        }

        private void OnDestroy()
        {
            _model.LoyaltyChanged -= _view.OnLoyaltyChanged;
        }
    }
}