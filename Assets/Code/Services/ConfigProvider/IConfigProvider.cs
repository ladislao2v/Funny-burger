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
<<<<<<< HEAD:Assets/Code/Services/StaticDataService/IStaticDataService.cs
        RecipeConfig[] GetRecipes();
=======
        IEnumerable<Recipe> GetRecipes();
>>>>>>> 3f2981a6ce365d14b31d0e298b0e3d3d8fd23979:Assets/Code/Services/ConfigProvider/IConfigProvider.cs
    }
}