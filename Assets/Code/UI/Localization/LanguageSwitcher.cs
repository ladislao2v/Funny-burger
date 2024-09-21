using Code.Services.LocalizationService;
using UnityEngine;
using Zenject;

namespace Code.UI.Localization
{
    public sealed class LanguageSwitcher : MonoBehaviour
    {
        [SerializeField] private LanguageButton _englishButton;
        [SerializeField] private LanguageButton _russianButton;
        
        private ILocalizationService _localizationService;

        [Inject]
        private void Construct(ILocalizationService localizationService) => 
            _localizationService = localizationService;

        private void OnEnable()
        {
            _englishButton.Clicked += OnEnglishButtonClicked;
            _englishButton.Clicked += _localizationService.ChangeLanguage;
            _russianButton.Clicked += OnRussianButtonClicked;
            _russianButton.Clicked += _localizationService.ChangeLanguage;
            
            _localizationService.LanguageChanged += OnLanguageChanged;
            
            OnLanguageChanged(_localizationService.Current);
        }

        private void OnDisable()
        {
            _englishButton.Clicked -= OnEnglishButtonClicked;
            _englishButton.Clicked -= _localizationService.ChangeLanguage;
            _russianButton.Clicked -= OnRussianButtonClicked;
            _russianButton.Clicked -= _localizationService.ChangeLanguage;
            
            _localizationService.LanguageChanged -= OnLanguageChanged;
        }

        private void OnRussianButtonClicked()
        {
            _englishButton.Unpick();
            _russianButton.Pick();
        }

        private void OnEnglishButtonClicked()
        {
            _russianButton.Unpick();
            _englishButton.Pick();
        }

        private void OnLanguageChanged(Language language)
        {
            if(language == Language.English)
                OnEnglishButtonClicked();
            else
                OnRussianButtonClicked();
        }
    }
}