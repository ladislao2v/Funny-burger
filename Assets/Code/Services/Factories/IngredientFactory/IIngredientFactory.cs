using Code.Goods;
using Code.Ingredients;
using Cysharp.Threading.Tasks;

namespace Code.Services.Factories.IngredientFactory
{
    public interface IIngredientFactory
    {
        UniTask<Ingredient> Create(IngredientType ingredientType);
    }
}