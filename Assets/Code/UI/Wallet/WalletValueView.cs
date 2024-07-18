using TMPro;
using UnityEngine;

namespace Code.UI.Wallet
{
    public class WalletValueView : View, IWalletView
    {
        [SerializeField] private TextMeshProUGUI _counter;
        
        public void OnValueChanged(int value)
        {
            _counter.text = $"{value}";
        }
    }
}