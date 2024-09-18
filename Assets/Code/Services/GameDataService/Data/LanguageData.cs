using System;
using Code.Services.LocalizationService;

namespace Code.Services.GameDataService.Data
{
    [Serializable]
    public class LanguageData : IData
    {
        public Language Language { get; set; }

        public LanguageData(Language language)
        {
            Language = language;
        }
    }
}