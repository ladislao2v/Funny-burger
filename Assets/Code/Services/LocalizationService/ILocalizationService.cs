using System;
using Code.Services.GameDataService;

namespace Code.Services.LocalizationService
{
    public interface ILocalizationService : ISavable
    {
        Language Current { get; }
        event Action<Language> LanguageChanged;

        void ChangeLanguage();
    }
}
