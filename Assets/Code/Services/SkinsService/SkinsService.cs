using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Services.ConfigProvider;
using Code.Services.GameDataService;
using Code.Services.GameDataService.Data;
using Code.Skins;

namespace Code.Services.SkinsService
{
    public class SkinsService : ISkinsService, ISavable
    {
        private readonly HashSet<BodySkinConfig> _openedBodySkinConfigs;
        private readonly HashSet<HatSkinConfig> _openedHatSkinConfigs;
        
        private readonly IConfigProvider _configProvider;
        
        public BodySkinConfig CurrentBodySkin { get; private set; }
        public HatSkinConfig CurrentHatSkin { get; private set; }
        
        public string SaveKey => nameof(SkinsService);

        public event Action<BodySkinConfig> BodySkinChanged;
        public event Action<HatSkinConfig> HatSkinChanged;

        public IEnumerable<BodySkinConfig> OpenedBodySkins => _openedBodySkinConfigs;
        public IEnumerable<HatSkinConfig> OpenedHatSkin => _openedHatSkinConfigs;

        public SkinsService(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void OpenNewBodySkinConfig(BodySkinConfig bodySkinConfig)
        {
            if(_openedBodySkinConfigs.Contains(bodySkinConfig))
                throw new ArgumentException(nameof(bodySkinConfig));
            
            _openedBodySkinConfigs.Add(bodySkinConfig);
            
            ChangeBodySkin(bodySkinConfig);
        }
        
        public void OpenNewHatSkinConfig(HatSkinConfig hatSkinConfig)
        {
            if(_openedHatSkinConfigs.Contains(hatSkinConfig))
                throw new ArgumentException(nameof(hatSkinConfig));
            
            _openedHatSkinConfigs.Add(hatSkinConfig);
            
            ChangeHatSkin(hatSkinConfig);
        }

        public bool TryUseBodySkin(BodySkinType bodySkinType)
        {
            var bodySkin = _openedBodySkinConfigs.FirstOrDefault(x => x.BodySkinId == bodySkinType);
            
            if(bodySkin == null)
                return false;
            
            ChangeBodySkin(bodySkin);
            
            return true;
        }
        
        public bool TryUseHatSkin(HatSkinType hatSkinType)
        {
            var hatSkin = _openedHatSkinConfigs.FirstOrDefault(x => x.HatSkinId == hatSkinType);
            
            if(hatSkin == null)
                return false;
            
            ChangeHatSkin(hatSkin);
            
            return true;
        }


        private void ChangeBodySkin(BodySkinConfig bodySkin)
        {
            CurrentBodySkin = bodySkin;
            
            BodySkinChanged?.Invoke(bodySkin);
        }

        private void ChangeHatSkin(HatSkinConfig hatSkin)
        {
            CurrentHatSkin = hatSkin;
            
            HatSkinChanged?.Invoke(hatSkin);
        }

        public void Load(IData data)
        {
            if (data == null)
                return;

            if (data is not SkinData skinData)
                throw new ArgumentException(nameof(data));
            
            BodySkinType bodySkinType = skinData.LastBodySkinType;
            HatSkinType hatSkinType = skinData.LastHatSkinType;
            List<BodySkinType> bodySkinTypes = skinData.OpenedBodySkins;
            List<HatSkinType> hatSkinTypes = skinData.OpenedHatSkins;

            CurrentBodySkin = _configProvider.GetBodySkinConfig(bodySkinType);
            CurrentHatSkin = _configProvider.GetHatSkinConfig(hatSkinType);
            _openedBodySkinConfigs.Concat(_configProvider
                .GetBodySkinConfigs()
                .Where(x => bodySkinTypes.Contains(x.BodySkinId))
                .ToList());
            _openedHatSkinConfigs.Concat(_configProvider
                .GetHatSkinConfigs()
                .Where(x => hatSkinTypes.Contains(x.HatSkinId))
                .ToList());
            
            BodySkinChanged?.Invoke(CurrentBodySkin);
            HatSkinChanged?.Invoke(CurrentHatSkin);
        }

        public IData Save() => new SkinData()
            {
                LastBodySkinType = CurrentBodySkin.BodySkinId,
                LastHatSkinType = CurrentHatSkin.HatSkinId,
                OpenedBodySkins = _openedBodySkinConfigs.Select(x => x.BodySkinId).ToList(),
                OpenedHatSkins = _openedHatSkinConfigs.Select(x => x.HatSkinId).ToList(),
            };
    }
}