using System.Threading.Tasks;
using Code.Configs;
using Code.Goods;
using Code.Services.PopupService;
using Cysharp.Threading.Tasks;

namespace Code.Services.StaticDataService
{
    public interface IStaticDataService
    {
        SettingsConfig SettingsConfig { get; }
        IngredientConfig GetIngredientConfig(IngredientType ingredientType);
        PopupConfig GetPopupConfig(PopupType popupType);
        RecipeConfig[] GetRecipes();
    }
}