using System;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;

namespace Code.Services.LocalizationService
{
    public sealed class LocalizationService : ILocalizationService
    {
        private Language _current = Language.English;
        
        public string SaveKey => nameof(LocalizationService);

        public Language Current => _current;
        
        public event Action<Language> LanguageChanged;

        public void ChangeLanguage()
        {
            if (_current == Language.English)
                _current = Language.Russian;
            else
                _current = Language.English;

            LanguageChanged?.Invoke(_current);
        }

        public void Load(IData data)
        {
            if (data == null)
                data = new LanguageData(Language.English);
            
            if (data is not LanguageData languageData)
                throw new ArgumentException(nameof(data));

            _current = languageData.Language;
            LanguageChanged?.Invoke(languageData.Language);
        }

        public IData Save() => 
            new LanguageData(_current);
    }
}