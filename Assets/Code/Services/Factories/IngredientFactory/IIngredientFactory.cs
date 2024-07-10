using Code.Goods;
using Code.Ingredients;

namespace Code.Services.Factories.IngredientFactory
{
    public interface IIngredientFactory
    {
        Ingredient Create(IngredientType ingredientType);
    }
}