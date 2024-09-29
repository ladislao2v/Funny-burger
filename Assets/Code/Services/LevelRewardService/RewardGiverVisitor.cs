using Code.Configs;
using Code.Services.RecipeService;
using Code.Services.ResourceStorage;

namespace Code.Services.LevelRewardService
{
    public sealed class RewardGiverVisitor : IItemVisitor
    {
        private readonly IResourceStorage _resourceStorage;
        private readonly IRecipeService _recipeService;

        public RewardGiverVisitor(IResourceStorage resourceStorage, IRecipeService recipeService)
        {
            _resourceStorage = resourceStorage;
            _recipeService = recipeService;
        }
        
        public void Visit(RecipeConfig recipeConfig) => 
            _recipeService.AddRecipe(recipeConfig);

        public void Visit(GemConfig gemConfig) =>
            _resourceStorage
                .GetWallet(ResourceType.Gem)
                .Add(gemConfig.Price);
    }
}