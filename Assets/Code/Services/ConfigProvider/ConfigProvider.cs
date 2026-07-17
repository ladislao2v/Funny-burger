using System;
using System.Collections.Generic;
using System.Linq;
using Code.Configs;
using Code.Constants;
using Code.Ingredients;
using Code.Services.PopupService;
using Code.Skins;
using UnityEngine;

namespace Code.Services.ConfigProvider
{
    public sealed class ConfigProvider : IConfigProvider
    {
        private readonly Dictionary<IngredientType,IngredientConfig> _ingredientsConfigs;
        private readonly Dictionary<PopupType, PopupConfig> _popupConfigs;
        private readonly Dictionary<BodySkinType, BodySkinConfig> _bodySkinConfigs;
        private readonly Dictionary<HatSkinType, HatSkinConfig> _hatSkinConfigs;
        private readonly RecipeConfig[] _recipes;
        private readonly GemConfig[] _gems;
        private readonly CoinConfig[] _coins;
        private readonly ShopItemConfig[] _shopItems;

        public SettingsConfig SettingsConfig { get; private set; }
        public LevelsRewardsConfig RewardsConfig { get; private set; }


        public ConfigProvider()
        {
            SettingsConfig = Resources
                .LoadAll<SettingsConfig>(ResourcePath.Settings)
                .FirstOrDefault();
            
            RewardsConfig = Resources
                .LoadAll<LevelsRewardsConfig>(ResourcePath.Settings)
                .FirstOrDefault();
            
            _ingredientsConfigs = Resources
                .LoadAll<IngredientConfig>(ResourcePath.Ingredients)
                .ToDictionary(x => x.Type);

            _popupConfigs = Resources
                .LoadAll<PopupConfig>(ResourcePath.Popups)
                .ToDictionary(x => x.Type);
            
            _bodySkinConfigs = Resources
                .LoadAll<BodySkinConfig>(ResourcePath.BodySkins)
                .ToDictionary(x => x.BodySkinId);
            
            _hatSkinConfigs = Resources
                .LoadAll<HatSkinConfig>(ResourcePath.HatSkins)
                .ToDictionary(x => x.HatSkinId);
            
            _shopItems = Resources
                .LoadAll<ShopItemConfig>(ResourcePath.ShopItems)
                .ToArray();

            _recipes = Resources
                .LoadAll<RecipeConfig>(ResourcePath.Recipes)
                .ToArray();
            
            _gems = Resources
                .LoadAll<GemConfig>(ResourcePath.Gems)
                .ToArray();
            
            _coins = Resources
                .LoadAll<CoinConfig>(ResourcePath.Coins)
                .ToArray();
        }

        public IngredientConfig GetIngredientConfig(IngredientType ingredientType)
        {
            if (_ingredientsConfigs.TryGetValue(ingredientType, out var config) == false)
                throw new ArgumentException(nameof(ingredientType));

            return config;
        }

        public PopupConfig GetPopupConfig(PopupType popupType)
        {
            if (_popupConfigs.TryGetValue(popupType, out var config) == false)
                throw new ArgumentException(nameof(popupType));

            return config;
        }

        public IEnumerable<BodySkinConfig> GetBodySkinConfigs() => _bodySkinConfigs.Values;

        public IEnumerable<HatSkinConfig> GetHatSkinConfigs() => _hatSkinConfigs.Values;

        public BodySkinConfig GetBodySkinConfig(BodySkinType bodySkinType)
        {
            if (_bodySkinConfigs.TryGetValue(bodySkinType, out var config) == false)
                throw new ArgumentException(nameof(bodySkinType));

            return config;
        }

        public HatSkinConfig GetHatSkinConfig(HatSkinType hatSkinType)
        {
            if (_hatSkinConfigs.TryGetValue(hatSkinType, out var config) == false)
                throw new ArgumentException(nameof(hatSkinType));

            return config;
        }

        public IEnumerable<RecipeConfig> GetRecipes() => _recipes;
        public IEnumerable<GemConfig> GetGems() => _gems;
        public IEnumerable<CoinConfig> GetCoins() => _coins;
        public IEnumerable<ShopItemConfig> GetShopItems() => _shopItems;
    }
}