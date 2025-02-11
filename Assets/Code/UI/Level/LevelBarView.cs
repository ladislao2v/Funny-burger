using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Level
{
    public class LevelBarView : View, ILevelBarView
    {
        [SerializeField] private Slider _bar;
        [SerializeField] private TextMeshProUGUI _current;
        [SerializeField] private TextMeshProUGUI _next;
        [SerializeField] private TextMeshProUGUI _progress;

        public void OnProgressChanged(int from, int to)
        {
            _progress.text = $"{from}/{to}";
            _bar.value = (float) from / to;
        }

        public void OnLevelChanged(int current, int next)
        {
            _current.text = current.ToString();
            _next.text = next.ToString();
        }
    }
}