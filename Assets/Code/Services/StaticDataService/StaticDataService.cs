using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Constants;
using Code.Goods;
using Code.Recipes;
using UnityEngine;

namespace Code.Services.StaticDataService
{
    public sealed class StaticDataService : IStaticDataService
    {
        private Dictionary<IngredientType,IngredientConfig> _configs;
        private Recipe[] _recipes;
        
        public SettingsConfig SettingsConfig { get; private set; }

        public StaticDataService()
        {
            SettingsConfig = Resources
                .LoadAll<SettingsConfig>(ResourcePathes.Settings)
                .FirstOrDefault();
            
            _configs = Resources
                .LoadAll<IngredientConfig>(ResourcePathes.Ingredients)
                .ToDictionary(x => x.Type);

            _recipes = Resources
                .LoadAll<Recipe>(ResourcePathes.Recipes)
                .ToArray();
        }
        
        public IngredientConfig GetIngredientConfig(IngredientType ingredientType)
        {
            if (_configs.ContainsKey(ingredientType) == false)
                throw new ArgumentException(nameof(ingredientType));

            return _configs[ingredientType];
        }

        public Recipe[] GetRecipes() => 
            _recipes;
    }
}