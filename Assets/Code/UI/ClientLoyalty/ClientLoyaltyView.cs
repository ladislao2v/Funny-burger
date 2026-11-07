using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.ClientLoyalty
{
    public class ClientLoyaltyView : View, IClientLoyaltyView
    {
        private const string TextTemplate = "{0}%";
        
        private const float Duration = 0.5f;
        
        [SerializeField] private Slider _bar;
        [SerializeField] private TMP_Text _percent;
        
        public void OnLoyaltyChanged(float value)
        {
            _bar.DOValue(value, Duration);
            _percent.text = string.Format(TextTemplate, Mathf.RoundToInt(value * 100f));
        }
    }
}