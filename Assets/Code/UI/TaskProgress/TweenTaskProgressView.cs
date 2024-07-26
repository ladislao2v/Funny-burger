using Code.Constants;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.TaskProgress
{
    public class TweenTaskProgressView : View, ITaskProgressView
    {
        [SerializeField] private Image _bar;

        public void OnCommandProgressed(float duration)
        {
            _bar.fillAmount = ProgressBar.Min;
            _bar.DOFillAmount(ProgressBar.Max, duration);
        }
    }
}