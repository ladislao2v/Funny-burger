using Code.Configs;
using Code.Goods;
using Code.Recipes;

namespace Code.Services.StaticDataService
{
    public interface IStaticDataService
    {
        GameConfig GameConfig { get; }
        IngredientConfig GetIngredientConfig(IngredientType ingredientType);
        Recipe[] GetRecipes();
    }
}