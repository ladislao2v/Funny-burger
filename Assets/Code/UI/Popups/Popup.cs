using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Popups
{
    public abstract class Popup : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void Start() => 
            _closeButton.onClick.AddListener(Close);

        private void OnDestroy() => 
            _closeButton.onClick.RemoveListener(Close);

        public void Close() => Destroy(this);
    }
}