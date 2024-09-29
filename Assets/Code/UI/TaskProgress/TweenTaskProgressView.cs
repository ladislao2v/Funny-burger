using Code.Constants;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.UI.TaskProgress
{
    public class TweenTaskProgressView : MonoBehaviour, ITaskProgressView
    {
        [SerializeField] private Image _bar;
        [SerializeField] private GameObject _background;

        public void OnCommandProgressed(float duration)
        {
            _bar.fillAmount = ProgressBar.Min;
            _bar.DOFillAmount(ProgressBar.Max, duration);
        }

        public void Show() => 
            _background.SetActive(true);

        public void Hide() => 
            _background.SetActive(false);
    }
}