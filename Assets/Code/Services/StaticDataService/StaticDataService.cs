using System;
using System.Collections.Generic;
using System.Linq;
using Code.Constants;
using Code.Goods;
using UnityEngine;

namespace Code.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<IngredientType,IngredientConfig> _configs;

        public StaticDataService()
        {
            _configs = Resources
                .LoadAll<IngredientConfig>(ResourcePathes.Configs)
                .ToDictionary(x => x.Type);
        }
        
        public IngredientConfig GetConfig(IngredientType ingredientType)
        {
            if (_configs.ContainsKey(ingredientType) == false)
                throw new ArgumentException(nameof(ingredientType));

            return _configs[ingredientType];
        }
    }
}