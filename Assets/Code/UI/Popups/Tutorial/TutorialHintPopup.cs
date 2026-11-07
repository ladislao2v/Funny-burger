using TMPro;
using UnityEngine;

namespace Code.UI.Popups.Tutorial
{
    public sealed class TutorialHintPopup : Popup
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;

        private void Start()
        {
            if (Data == null)
                return;

            _title.text = Data.Name;
            _description.text = Data.Description;
        }
    }
}
