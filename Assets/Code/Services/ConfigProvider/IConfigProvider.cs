using System.Collections.Generic;
using Code.Configs;
using Code.Goods;
using Code.Services.PopupService;

namespace Code.Services.ConfigProvider
{
    public interface IConfigProvider
    {
        SettingsConfig SettingsConfig { get; }
        IngredientConfig GetIngredientConfig(IngredientType ingredientType);
        PopupConfig GetPopupConfig(PopupType popupType);

        IEnumerable<RecipeConfig> GetRecipes();
    }
}