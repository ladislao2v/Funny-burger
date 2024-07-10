using Code.Goods;
using Code.Ingredients;
using Code.Services.Factories.PrefabFactory;
using Code.Services.StaticDataService;

namespace Code.Services.Factories.IngredientFactory
{
    public class IngredientFactory : IIngredientFactory
    {
        private readonly IPrefabFactory _prefabFactory;
        private readonly IStaticDataService _staticDataService;

        public IngredientFactory(IPrefabFactory prefabFactory, IStaticDataService staticDataService)
        {
            _prefabFactory = prefabFactory;
            _staticDataService = staticDataService;
        }
        
        public Ingredient Create(IngredientType ingredientType)
        {
            IngredientConfig config = _staticDataService.GetConfig(ingredientType);
            Ingredient ingredient = _prefabFactory.Create(config.Prefab);

            return ingredient;
        }
    }
}