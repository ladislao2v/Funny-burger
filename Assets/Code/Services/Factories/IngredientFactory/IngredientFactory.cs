using Code.Configs;
using Code.Goods;
using Code.Ingredients;
using Code.Services.Factories.PrefabFactory;
using Code.Services.StaticDataService;
using Cysharp.Threading.Tasks;

namespace Code.Services.Factories.IngredientFactory
{
    public sealed class IngredientFactory : IIngredientFactory
    {
        private readonly IPrefabFactory _prefabFactory;
        private readonly IStaticDataService _staticDataService;

        public IngredientFactory(IPrefabFactory prefabFactory, IStaticDataService staticDataService)
        {
            _prefabFactory = prefabFactory;
            _staticDataService = staticDataService;
        }
        
        public async UniTask<Ingredient> Create(IngredientType ingredientType)
        {
            IngredientConfig config = _staticDataService.GetIngredientConfig(ingredientType);
            Ingredient ingredient = await _prefabFactory.Create<Ingredient>(config.AssetReference);

            return ingredient;
        }
    }
}