using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Localization
{
    public sealed class LanguageButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _picked;

        public event Action Clicked;

        public void Pick() => 
            _picked.SetActive(true);

        public void Unpick() => 
            _picked.SetActive(false);

        private void OnEnable() => 
            _button.onClick.AddListener(OnClick);

        private void OnDisable() => 
            _button.onClick.AddListener(OnClick);

        private void OnClick() => 
            Clicked?.Invoke();
    }
}