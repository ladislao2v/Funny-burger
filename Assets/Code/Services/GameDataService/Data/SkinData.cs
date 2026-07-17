using System;
using System.Collections.Generic;
using Code.Skins;

namespace Code.Services.GameDataService.Data
{
    [Serializable]
    public class SkinData : IData
    {
        public BodySkinType LastBodySkinType;
        public HatSkinType LastHatSkinType;
        
        public List<BodySkinType> OpenedBodySkins;
        public List<HatSkinType> OpenedHatSkins;
    }
}