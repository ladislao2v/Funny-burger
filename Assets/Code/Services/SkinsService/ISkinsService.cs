using System;
using System.Collections.Generic;
using Code.Configs;
using Code.Skins;
using UnityEngine;

namespace Code.Services.SkinsService
{
    public interface ISkinsService
    {
        BodySkinConfig CurrentBodySkin { get; }
        HatSkinConfig CurrentHatSkin { get; }
        IEnumerable<BodySkinConfig> OpenedBodySkins { get; }
        IEnumerable<HatSkinConfig> OpenedHatSkin { get; }

        event Action<BodySkinConfig> BodySkinChanged;
        event Action<HatSkinConfig> HatSkinChanged;
        void OpenNewBodySkinConfig(BodySkinConfig bodySkinConfig);
        void OpenNewHatSkinConfig(HatSkinConfig hatSkinConfig);
        bool TryUseBodySkin(BodySkinType bodySkinType);
        bool TryUseHatSkin(HatSkinType hatSkinType);
    }
}