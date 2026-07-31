using System.Collections.Generic;
using Code.Configs;
using Code.Ingredients;
using Code.Services.PopupService;
using Code.Services.ShopService;
using Code.Skins;

namespace Code.Services.ConfigProvider
{
    public interface IConfigProvider
    {
        SettingsConfig SettingsConfig { get; }
        LevelsRewardsConfig RewardsConfig { get; }
        IngredientConfig GetIngredientConfig(IngredientType ingredientType);
        PopupConfig GetPopupConfig(PopupType popupType);
        
        IEnumerable<BodySkinConfig> GetBodySkinConfigs();
        IEnumerable<HatSkinConfig> GetHatSkinConfigs();
        BodySkinConfig GetBodySkinConfig(BodySkinType bodySkinType);
        HatSkinConfig GetHatSkinConfig(HatSkinType hatSkinType);

        IEnumerable<RecipeConfig> GetRecipes();
        IEnumerable<GemConfig> GetGems();
        IEnumerable<ShopItemConfig> GetShopItems();
        ShopItemConfig GetShopItemConfigByItem(Item item);
        IEnumerable<ShopItemConfig> GetShopItemsByType(ShopType shopType);
    }
}