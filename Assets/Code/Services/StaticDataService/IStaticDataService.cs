using Code.Goods;
using Code.Recipes;

namespace Code.Services.StaticDataService
{
    public interface IStaticDataService
    {
        IngredientConfig GetIngredientConfig(IngredientType ingredientType);
        Recipe[] GetRecipes();
    }
}