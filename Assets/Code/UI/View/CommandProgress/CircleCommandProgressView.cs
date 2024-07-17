using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.View.CommandProgress
{
    public class CircleCommandProgressView : View, ICommandProgressView
    {
        [SerializeField] private Image _bar;

        private void Awake()
        {
            _bar = GetComponent<Image>();
        }

        public void OnCommandProgressed(float progress)
        {
            _bar.fillAmount = progress;
        }
    }
}