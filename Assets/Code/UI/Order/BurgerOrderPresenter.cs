using Code.Services.BurgerOrderService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.UI.Order
{
    public class BurgerOrderPresenter : MonoBehaviour
    {
        private IBurgerOrderService _model;
        private IOrderView _view;

        [Inject]
        private void Construct(IBurgerOrderService burgerOrderService)
        {
            _model = burgerOrderService;
            _view = GetComponent<IOrderView>();

            _model.Ordered += _view.OnOrder;
            _model.Failed += _view.Hide;
            _model.OrderPassed += _view.Hide;
        }


        private void OnDestroy()
        {
            _model.Ordered += _view.OnOrder;
            _model.Failed += _view.Hide;
            _model.OrderPassed += _view.Hide;
        }
    }
}