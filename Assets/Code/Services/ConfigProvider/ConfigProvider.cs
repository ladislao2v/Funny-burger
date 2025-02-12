﻿using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Constants;
using Code.Ingredients;
using Code.Services.PopupService;
using UnityEngine;

namespace Code.Services.ConfigProvider
{
    public sealed class ConfigProvider : IConfigProvider
    {
        private readonly Dictionary<IngredientType,IngredientConfig> _ingredientsConfigs;
        private readonly Dictionary<PopupType, PopupConfig> _popupConfigs;
        private readonly RecipeConfig[] _recipes;
        private readonly GemConfig[] _gems;

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
                .LoadAll<RecipeConfig>(ResourcePath.Recipes)
                .ToArray();
            
            _gems = Resources
                .LoadAll<GemConfig>(ResourcePath.Gems)
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

        public IEnumerable<RecipeConfig> GetRecipes() => _recipes;
        public IEnumerable<GemConfig> GetGems() => _gems;
    }
}