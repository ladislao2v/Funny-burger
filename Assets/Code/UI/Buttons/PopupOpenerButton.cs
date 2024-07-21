using Code.Services.PopupService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class PopupOpenerButton : MonoBehaviour
    {
        [SerializeField] private PopupType _popupType;
        
        private IPopupService _popupService;
        private Button _button;

        [Inject]
        private void Construct(IPopupService popupService)
        {
            _popupService = popupService;
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnClicked() => 
            _popupService.ShowPopup(_popupType);
    }
}