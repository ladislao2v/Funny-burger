using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.UI.Wallet
{
    public class WalletValueView : View, IWalletView
    {
        private const float Duration = 1f;
        
        [SerializeField] private TextMeshProUGUI _counter;
        
        public void OnValueChanged(int value)
        {
            int lastValue = int.Parse(_counter.text);
            _counter.DOCounter(lastValue, value, Duration);
        }
    }
}