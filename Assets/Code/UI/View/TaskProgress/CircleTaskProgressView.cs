using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.View.TaskProgressView
{
    public class CircleTaskProgressView : MonoBehaviour, ITaskProgressView
    {
        [SerializeField] private Image _bar;

        private void Awake()
        {
            _bar = GetComponent<Image>();
        }

        public void OnTaskProgressChanged(float progress)
        {
            _bar.fillAmount = progress;
        }
    }
}