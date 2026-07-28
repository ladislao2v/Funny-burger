using System;
using Code.Configs;
using Code.Services.SkinsService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.ActiveSkins
{
    public class ActiveSkinsBar : MonoBehaviour
    {
        [SerializeField] private IconButton _hatSkinButton;
        [SerializeField] private IconButton _bodySkinButton;
        
        private ISkinsService _skinsService;

        [Inject]
        private void Construct(ISkinsService skinsService)
        {
            _skinsService = skinsService;
        }
        
        private void Start()
        {
            OnBodySkinChanged(_skinsService.CurrentBodySkin);
            OnHatSkinChanged(_skinsService.CurrentHatSkin);
        }

        private void OnEnable()
        {
            _skinsService.BodySkinChanged += OnBodySkinChanged;
            _skinsService.HatSkinChanged += OnHatSkinChanged;
        }

        private void OnDisable()
        {
            _skinsService.BodySkinChanged -= OnBodySkinChanged;
            _skinsService.HatSkinChanged -= OnHatSkinChanged;
        }

        private void OnBodySkinChanged(BodySkinConfig skin) => 
            _bodySkinButton.Icon.sprite = skin.Logo;

        private void OnHatSkinChanged(HatSkinConfig skin) => 
            _hatSkinButton.Icon.sprite = skin.Logo;

        public void SubscribeToButtons(UnityAction bodyIconClicked, UnityAction hatIconClicked)
        {
            _bodySkinButton.Button.onClick.AddListener(bodyIconClicked);
            _hatSkinButton.Button.onClick.AddListener(hatIconClicked);
        }
        
        public void UnsubscribeFromButtons()
        {
            _bodySkinButton.Button.onClick.RemoveAllListeners();
            _hatSkinButton.Button.onClick.RemoveAllListeners();
        }
    }

    [Serializable]
    public class IconButton
    {
        public Image Icon;
        public Button Button;
    }
}