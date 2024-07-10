using Code.Goods;

namespace Code.Services.StaticDataService
{
    public interface IStaticDataService
    {
        IngredientConfig GetConfig(IngredientType ingredientType);
    }
}