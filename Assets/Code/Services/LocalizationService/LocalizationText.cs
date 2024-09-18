using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Services.LocalizationService
{
    public sealed class LocalizationText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        [Space, Header("Translates")] 
        [SerializeField] private TranslateData _translateData;
        
        private ILocalizationService _localizationService;

        [Inject]
        private void Construct(ILocalizationService localizationService)
        {
            _localizationService = localizationService;

            OnLanguageChanged(_localizationService.Current);
        }

        private void OnEnable() => 
            _localizationService.LanguageChanged += OnLanguageChanged;

        private void OnDisable() => 
            _localizationService.LanguageChanged -= OnLanguageChanged;

        private void OnLanguageChanged(Language language) => 
            _text.text = _translateData[language];
    }
}