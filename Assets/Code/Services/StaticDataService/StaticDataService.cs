using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Constants;
using Code.Goods;
using Code.Services.PopupService;
using UnityEngine;

namespace Code.Services.StaticDataService
{
    public sealed class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<IngredientType,IngredientConfig> _ingredientsConfigs;
        private readonly Dictionary<PopupType, PopupConfig> _popupConfigs;
        private readonly RecipeConfig[] _recipes;

        public SettingsConfig SettingsConfig { get; private set; }

        public StaticDataService()
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
                .LoadAll<RecipeConfig>(ResourcePath.Recipes)
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

        public RecipeConfig[] GetRecipes() => 
            _recipes;
    }
}