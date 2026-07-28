using TMPro;
using UnityEngine;

namespace Code.UI.Popups.Reward
{
    public sealed class RewardPopup : Popup
    {
        [SerializeField] private TMP_Text _label;
        
        private void Start()
        { 
            Initialize(Data);
        }

        private void Initialize(RewardData data) => 
            _label.text = string.Format(_label.text, data.Name);
    }
}