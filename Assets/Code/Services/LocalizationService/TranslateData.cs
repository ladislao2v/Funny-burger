using System;
using System.Linq;

namespace Code.Services.LocalizationService
{
    [Serializable]
    public class TranslateData
    {
        public WordData[] Words;

        public string this[Language language]
        {
            get { return Words
                .FirstOrDefault(x => x.Language == language)?.Word; 
            }
        }
    }
}