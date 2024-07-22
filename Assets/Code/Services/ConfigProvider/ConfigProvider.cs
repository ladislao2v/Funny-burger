using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Constants;
using Code.Goods;
using Code.Recipes;
using Code.Services.PopupService;
using UnityEngine;

namespace Code.Services.ConfigProvider
{
    public sealed class ConfigProvider : IConfigProvider
    {
        private readonly Dictionary<IngredientType,IngredientConfig> _ingredientsConfigs;
        private readonly Dictionary<PopupType, PopupConfig> _popupConfigs;
        private readonly Recipe[] _recipes;

        public SettingsConfig SettingsConfig { get; private set; }

        public ConfigProvider()
        {
            SettingsConfig = Resources
                .LoadAll<SettingsConfig>(ResourcePath.Settings)
                .FirstOrDefault();
            
            _ingredientsConfigs = Resources
                .LoadAll<IngredientConfig>(ResourcePath.Ingredients)
                .ToDictionary(x => x.Type);

            _popupConfigs = Resources
                .LoadAll<PopupConfig>(ResourcePath.Popups)
                .ToDictionary(x => x.Type);

            _recipes = Resources
                .LoadAll<Recipe>(ResourcePath.Recipes)
                .ToArray();
        }
        
        public IngredientConfig GetIngredientConfig(IngredientType ingredientType)
        {
            if (_ingredientsConfigs.ContainsKey(ingredientType) == false)
                throw new ArgumentException(nameof(ingredientType));

            return _ingredientsConfigs[ingredientType];
        }

        public PopupConfig GetPopupConfig(PopupType popupType)
        {
            if (_popupConfigs.ContainsKey(popupType) == false)
                throw new ArgumentException(nameof(popupType));

            return _popupConfigs[popupType];
        }

        public IEnumerable<Recipe> GetRecipes() => 
            _recipes;
    }
}